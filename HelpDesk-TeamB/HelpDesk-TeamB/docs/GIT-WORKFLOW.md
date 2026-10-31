# Git iş prosesi

Repo: `HelpDesk-TeamB` (digər komandalar üçün `HelpDesk-TeamA`, `HelpDesk-TeamC` və s.). `main`-ə birbaşa push qadağandır.

| Rol | Branch | Fayllar |
|-----|--------|---------|
| Frontend | feature/frontend | Views/**, wwwroot/** |
| Backend | feature/backend | Models/Ticket.cs, Controllers/TicketController.cs |
| Database | feature/database | database.sql, Data/** |
| Security | feature/security | Controllers/AccountController.cs, Views/Account/** |
| QA | feature/testing | test-cases.md |
| Documentation | feature/documentation | README.md, .gitignore, docs/** |

```bash
git clone REPOSITORY_URL && cd HelpDesk-TeamB
git switch -c feature/<rol>
git status && git add . && git diff --staged
git commit -m "Add <nə edildi>"
git push -u origin feature/<rol>      # GitHub-da PR: feature/<rol> → main
```

## Merge conflict (README.md)
Başlanğıc: `Project Status: In Development`; Frontend → `Frontend Completed`; Backend → `Backend Completed`.
İkinci PR merge edilərkən konflikt yaranır:
```bash
git switch main && git pull origin main
git merge feature/backend
# markerləri silin, yekun: Project Status: Frontend and Backend Completed
git add README.md
git commit -m "Resolve README merge conflict"
git push origin main
```
Səbəb: iki branch eyni sətri fərqli dəyişib.

## Revert
```bash
git log --oneline
git revert <commit-hash>    # tarixçəni silmir, əks commit yaradır
git push origin main
```

## Əlavə əmrlər
`git stash`/`stash pop`, `git restore <fayl>`, `git show <hash>`, `git fetch`, `git tag v1.0`, `git mv`, `git rm`, `git blame <fayl>`.
