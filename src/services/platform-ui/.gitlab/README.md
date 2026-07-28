# UI apps — GitLab CI (`/.gitlab`)

This folder holds the GitLab pipeline definition for the BGP UI image (`game-portal-ui-bgp.gitlab-ci.yml`) and this catalog of **CI/CD variables**.

---

## 1. What are GitLab CI/CD variables?

**CI/CD variables** are key/value settings GitLab injects into your pipeline jobs as **environment variables**. Jobs read them in scripts as `$VARIABLE_NAME` (or `$env:VARIABLE_NAME` in PowerShell runners).

| Concept | Description |
|--------|-------------|
| **Predefined variables** | Set automatically by GitLab (e.g. `CI_PROJECT_DIR`, `CI_COMMIT_SHA`). You do not create them; you only use them. |
| **Custom variables** | Defined in **Project → Settings → CI/CD → Variables** (or group/instance level). Used for secrets, registry hosts, tokens. |
| **Job `variables:`** | Declared inside `.gitlab-ci.yml` under `variables:` or under a job. Visible in logs unless masked. |

Variables can be **masked** (hidden in job logs), **protected** (only on protected branches/tags), and scoped to **environments**.

---

## 2. Where they are configured (UI)

1. Open the GitLab project that runs the pipeline.
2. **Settings → CI/CD → Variables** → **Expand**.
3. **Add variable**: set key, value, and options (mask, protect, environment scope).

**Looks like in GitLab UI (field semantics):**

| UI field | Typical use |
|----------|-------------|
| **Key** | `GIT_TOKEN`, `PRREG_REGISTRY`, … (uppercase with underscores is common). |
| **Value** | Secret string or public config; use **Mask variable** for secrets. |
| **Type** | Usually **Variable** (file type exists for large secrets / kubeconfig). |
| **Flags** | **Protect** — only protected pipelines; **Mask** — do not print in logs; **Expand** — substitute `$OTHER` refs in value. |

---

## 3. Catalog — variables used by `game-portal-ui-bgp.gitlab-ci.yml`

These **custom** variables must exist on the GitLab project (or be inherited from a group) for the clone + Docker push flow to work.

| Variable | Required | Mask in GitLab? | Example value (illustrative) | Role |
|----------|----------|-----------------|------------------------------|------|
| `GIT_TOKEN` | Yes | Yes | `github_pat_xxxxxxxxxxxx` (GitHub fine-grained or classic PAT) | Authenticates `git clone` of `volnyja-siervisy` from GitHub over HTTPS. |
| `PRREG_REGISTRY` | Yes | No* | `registry.tools.volnyja-siervisy.site` | Docker registry hostname (no `https://` prefix for `docker login`). |
| `PRREG_REGISTRY_USER` | Yes | Often yes | `ci-push` | Registry username for `docker login`. |
| `PRREG_REGISTRY_PASSWORD` | Yes | Yes | `eyJhbGciOiJ...` or registry token | Registry password or token; piped to `docker login`. |

\*Hostnames are often left unmasked; mask if your policy treats them as sensitive.

**Usage in the pipeline (how they appear in scripts):**

```bash
git clone https://$GIT_TOKEN@github.com/aliaksei-aziauchykau/volnyja-siervisy.git vendor/repo
echo "$PRREG_REGISTRY_PASSWORD" | docker login -u "$PRREG_REGISTRY_USER" --password-stdin "$PRREG_REGISTRY"
docker push $PRREG_REGISTRY/bgp/bgp-ui:latest
```

---

## 4. Catalog — predefined GitLab variables this job relies on

You do **not** set these in Settings; GitLab provides them.

| Variable | Example | Role here |
|----------|---------|-----------|
| `CI_PROJECT_DIR` | `/builds/group/project` | Absolute path to the job workspace; used to build `DOCKER_FILE_PATH` / `DOCKER_CONTEXT_PATH` after clone. |
| `CI_JOB_TOKEN` | (per job) | Not used in this file; available for GitLab API/registry integration if you switch away from a static `GIT_TOKEN`. |

Full list: [Predefined variables (GitLab docs)](https://docs.gitlab.com/ee/ci/variables/predefined_variables.html).

---

## 5. Catalog — variables defined only in YAML (`game-portal-ui-bgp.gitlab-ci.yml`)

These are **not** GitLab Settings entries; they are fixed or composed in the pipeline file.

| Name | Set in YAML | Example / note |
|------|-------------|----------------|
| `GIT_STRATEGY` | Global `variables:` | `none` — skip default Git checkout; job clones explicitly. |
| `LIBRARY_PATH` | Global `variables:` | `vendor/repo` — directory name for the cloned repository. |
| `DOCKER_HOST` | Job `variables:` | `tcp://docker:2375` — Docker-in-Docker service. |
| `DOCKER_TLS_CERTDIR` | Job `variables:` | `""` — common with `docker:dind` when not using TLS to the daemon. |
| `IMAGE_NAME`, `IMAGE_TAG`, `IMAGE_PROJECT`, `IMAGE` | Job `variables:` | `bgp-ui` / `latest` / `bgp` — image name/tag and namespace for `docker tag` / `docker push`. |
| `DOCKER_FILE_PATH`, `DOCKER_CONTEXT_PATH` | Job `variables:` | Built from `CI_PROJECT_DIR` + clone path + `src/ui-apps` (Dockerfile under `.docker/development/`). |

---

## 6. Quick checklist before running the pipeline

- [ ] `GIT_TOKEN` can **read** the GitHub repo `aliaksei-aziauchykau/volnyja-siervisy`.
- [ ] `PRREG_*` match a registry the runner can reach; runner allows **Docker** and **push** for `bgp/bgp-ui`.
- [ ] Secrets are **masked** where possible; rotate tokens if they appear in logs.
- [ ] CI config path is set (root `.gitlab-ci.yml` with `include`, or **CI/CD configuration file** pointing at this `.yml`).

---

## 7. Related file

| File | Purpose |
|------|---------|
| `game-portal-ui-bgp.gitlab-ci.yml` | Job `build-bgp-ui`: clone repo, `docker build`, tag, push UI image to private registry. |

---

## 8. See also

| Location | Purpose |
|----------|---------|
| `src/game-portal/backend/.gitlab/` | Same pattern for **BGP API** (`build-bgp-api`, `bgp-api` image). |
