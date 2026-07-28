#Requires -Version 5.1
<#
.SYNOPSIS
    Bootstraps the local Keycloak realm `gamesapp` (clients, audience mapper, demo user).
#>
param(
    [string]$KeycloakBaseUrl = 'http://127.0.0.1:8180',
    [string]$AdminUser = 'admin',
    [string]$AdminPassword = 'admin',
    [string]$DemoUser = 'demo',
    [string]$DemoPassword = 'demo'
)

$ErrorActionPreference = 'Stop'

function Get-AdminToken {
    $body = @{
        client_id  = 'admin-cli'
        username   = $AdminUser
        password   = $AdminPassword
        grant_type = 'password'
    }
    $token = Invoke-RestMethod -Method Post `
        -Uri "$KeycloakBaseUrl/realms/master/protocol/openid-connect/token" `
        -Body $body -ContentType 'application/x-www-form-urlencoded'
    return $token.access_token
}

function Invoke-KeycloakAdmin {
    param(
        [string]$Method,
        [string]$Path,
        [object]$Body = $null
    )
    $headers = @{ Authorization = "Bearer $script:Token" }
    $uri = "$KeycloakBaseUrl/admin$Path"
    if ($null -eq $Body) {
        return Invoke-RestMethod -Method $Method -Uri $uri -Headers $headers
    }
    $json = $Body | ConvertTo-Json -Depth 10
    return Invoke-RestMethod -Method $Method -Uri $uri -Headers $headers -ContentType 'application/json' -Body $json
}

$script:Token = Get-AdminToken

try {
    Invoke-KeycloakAdmin -Method Get -Path '/realms/gamesapp' | Out-Null
    Write-Host 'Realm gamesapp already exists - skipping create.' -ForegroundColor Yellow
}
catch {
    Invoke-KeycloakAdmin -Method Post -Path '/realms' -Body @{
        realm                 = 'gamesapp'
        enabled               = $true
        registrationAllowed   = $false
        loginWithEmailAllowed = $true
        sslRequired           = 'none'
    }
    Write-Host 'Created realm gamesapp.' -ForegroundColor Green
}

$spaClient = @{
    clientId                     = 'gamesapp-spa'
    name                         = 'GamesApp SPA'
    enabled                      = $true
    publicClient                 = $true
    protocol                     = 'openid-connect'
    standardFlowEnabled          = $true
    directAccessGrantsEnabled    = $false
    redirectUris                 = @(
        'http://localhost:4220/*'
        'http://localhost:4221/*'
        'http://localhost:4200/*'
        'http://localhost:4201/*'
        'http://ui.gamesapp.local.projects.volnyja-siervisy.site:9080/*'
        'http://ui-dev.gamesapp.local.projects.volnyja-siervisy.site:9080/*'
    )
    webOrigins                   = @(
        'http://localhost:4220'
        'http://localhost:4221'
        'http://localhost:4200'
        'http://localhost:4201'
        'http://ui.gamesapp.local.projects.volnyja-siervisy.site:9080'
        'http://ui-dev.gamesapp.local.projects.volnyja-siervisy.site:9080'
    )
}

$apiClient = @{
    clientId   = 'gamesapp-api'
    name       = 'GamesApp API'
    enabled    = $true
    bearerOnly = $true
    protocol   = 'openid-connect'
}

foreach ($client in @($spaClient, $apiClient)) {
    $existing = Invoke-KeycloakAdmin -Method Get -Path "/realms/gamesapp/clients?clientId=$($client.clientId)"
    if ($existing.Count -gt 0) {
        Write-Host "Client $($client.clientId) already exists - updating." -ForegroundColor Yellow
        $id = $existing[0].id
        Invoke-KeycloakAdmin -Method Put -Path "/realms/gamesapp/clients/$id" -Body ($client + @{ id = $id })
    }
    else {
        Invoke-KeycloakAdmin -Method Post -Path '/realms/gamesapp/clients' -Body $client
        Write-Host "Created client $($client.clientId)." -ForegroundColor Green
    }
}

$spa = (Invoke-KeycloakAdmin -Method Get -Path '/realms/gamesapp/clients?clientId=gamesapp-spa')[0]
$mappers = Invoke-KeycloakAdmin -Method Get -Path "/realms/gamesapp/clients/$($spa.id)/protocol-mappers/models"
if (-not ($mappers | Where-Object { $_.name -eq 'gamesapp-api-audience' })) {
    Invoke-KeycloakAdmin -Method Post -Path "/realms/gamesapp/clients/$($spa.id)/protocol-mappers/models" -Body @{
        name           = 'gamesapp-api-audience'
        protocol       = 'openid-connect'
        protocolMapper = 'oidc-audience-mapper'
        config         = @{
            'included.client.audience' = 'gamesapp-api'
            'id.token.claim'           = 'true'
            'access.token.claim'       = 'true'
        }
    }
    Write-Host 'Added audience mapper on gamesapp-spa.' -ForegroundColor Green
}

try {
    Invoke-KeycloakAdmin -Method Get -Path "/realms/gamesapp/users?username=$DemoUser" | Out-Null
    $users = Invoke-KeycloakAdmin -Method Get -Path "/realms/gamesapp/users?username=$DemoUser"
    if ($users.Count -eq 0) {
        throw 'not found'
    }
    Write-Host "User $DemoUser already exists." -ForegroundColor Yellow
}
catch {
    Invoke-KeycloakAdmin -Method Post -Path '/realms/gamesapp/users' -Body @{
        username      = $DemoUser
        enabled       = $true
        emailVerified = $true
        email         = "$DemoUser@local.test"
        credentials   = @(
            @{
                type      = 'password'
                value     = $DemoPassword
                temporary = $false
            }
        )
    }
    Write-Host "Created demo user $DemoUser / $DemoPassword." -ForegroundColor Green
}

Write-Host "`nKeycloak ready: $KeycloakBaseUrl/realms/gamesapp" -ForegroundColor Cyan
Write-Host ('  SPA client: gamesapp-spa | API audience: gamesapp-api | Login: {0} / {1}' -f $DemoUser, $DemoPassword) -ForegroundColor Gray
