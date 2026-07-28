$containerName = "sqitch"
$workingDirectory = "/app"

docker exec --user root -it -w $workingDirectory  $containerName bash 