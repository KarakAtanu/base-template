# Testing Template Workflow - Fixed Version

## What Changed
- ✅ Removed auto-push that was causing permission errors (403)
- ✅ Workflow now creates local commits only
- ✅ Users manually push changes or use Git GUI

## How to Test (Step by Step)

### Step 1: Update order-service with Latest Workflow
Since order-service was created before the fix, it has the old workflow.

**Option A - Pull Latest Workflow (Recommended)**
```powershell
cd path/to/order-service
git remote add template https://github.com/KarakAtanu/base-template.git
git fetch template
git checkout template/main -- .github/workflows/template-setup.yml
git add .github/workflows/template-setup.yml
git commit -m "update: use fixed template workflow"
git push origin main
```

**Option B - Manual Update**
- Go to order-service on GitHub
- Update `.github/workflows/template-setup.yml` with the new version from base-template
- Commit the change

### Step 2: Re-Run Workflow
1. Go to order-service repo → **Actions** tab
2. Select **Template Setup** workflow
3. Click **Run workflow** → **Run workflow**
4. Wait for completion (2-3 minutes)

### Step 3: Verify Results
✅ Check Actions Log:
- Rename script should complete successfully
- Build should succeed
- Should see: "Template Rename Completed!" message

✅ Check Repository Files:
```
✓ OrderService.sln (not BaseTemplate.sln)
✓ OrderService.Domain/ folder
✓ OrderService.Application/ folder
✓ OrderService.Infrastructure/ folder
✓ OrderService.API/ folder
✓ OrderService.Tests/ folder
```

✅ Check Commits:
```
git log --oneline
# Should show: "chore: initialize project from template"
```

### Step 4: Push Changes to GitHub
The workflow creates commits locally. Now push them:

**Command Line:**
```powershell
git push origin main --force
```

**OR using Git GUI:**
- Open GitHub Desktop / VS Code
- Click "Push to origin" button

### Step 5: Final Verification
1. Clone repository fresh to verify:
```powershell
git clone https://github.com/YOUR-USERNAME/order-service.git
cd order-service
dotnet restore
dotnet build
```

2. Verify namespace updates:
```csharp
// Should show OrderService.* not BaseTemplate.*
using OrderService.Domain.Interfaces;
namespace OrderService.Infrastructure.Persistence;
```

## Troubleshooting

| Issue | Solution |
|-------|----------|
| Workflow still uses old file | Use "Option A" above to pull latest |
| Build fails after renaming | Run `git push` to sync all changes |
| Changes not appearing on GitHub | Changes are local only - must `git push` manually |
| Wrong project names | Ensure you triggered with repo name "order-service" |

## Complete Workflow Success Example
```
✅ Setup .NET (8.0.419)
✅ Run rename script
   Renamed 5 folders
   Renamed 5 .csproj files
   Renamed .sln file
   Updated 30+ namespaces
✅ Restore dependencies
✅ Build solution (all 5 projects)
✅ Commit changes (chore: initialize project from template)
✅ Template Rename Completed!
   → Run: git push origin main --force
```
