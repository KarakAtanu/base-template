param(
    [Parameter(Mandatory = $true)]
    [string]$RepositoryName
)

function ConvertTo-PascalCase {
    param([string]$inputString)
    
    # Replace hyphens and underscores with spaces, split, and rejoin with PascalCase
    $parts = $inputString -replace '[-_]', ' ' -split ' ' | ForEach-Object { 
        if ($_.Length -gt 0) {
            $_.Substring(0, 1).ToUpper() + $_.Substring(1).ToLower()
        }
    }
    return $parts -join ''
}

function Find-And-Replace-Recursive {
    param(
        [string]$rootPath,
        [string]$oldText,
        [string]$newText
    )
    
    Get-ChildItem -Path $rootPath -Include "*.cs", "*.csproj", "*.sln", "*.json" -Recurse | ForEach-Object {
        $content = Get-Content $_.FullName -Raw
        if ($content -match [regex]::Escape($oldText)) {
            $newContent = $content -replace [regex]::Escape($oldText), $newText
            Set-Content -Path $_.FullName -Value $newContent
            Write-Output "Updated: $($_.FullName)"
        }
    }
}

Write-Output "Starting template rename process..."
Write-Output "Repository Name: $RepositoryName"

# Convert to PascalCase
$projectName = ConvertTo-PascalCase -inputString $RepositoryName
Write-Output "Project Name: $projectName"

# Rename folders
$folders = @("Domain", "Application", "Infrastructure", "API", "Tests")
foreach ($folder in $folders) {
    $oldFolderPath = ".\BaseTemplate.$folder"
    $newFolderPath = ".\$projectName.$folder"
    
    if (Test-Path $oldFolderPath) {
        Rename-Item -Path $oldFolderPath -NewName "$projectName.$folder" -Force
        Write-Output "Renamed folder: BaseTemplate.$folder → $projectName.$folder"
    }
}

# Rename .csproj files
foreach ($folder in $folders) {
    $newFolderPath = ".\$projectName.$folder"
    $oldCsprojPath = "$newFolderPath\BaseTemplate.$folder.csproj"
    $newCsprojFileName = "$projectName.$folder.csproj"
    
    if (Test-Path $oldCsprojPath) {
        Rename-Item -Path $oldCsprojPath -NewName $newCsprojFileName -Force
        Write-Output "Renamed .csproj: BaseTemplate.$folder.csproj → $newCsprojFileName"
    }
}

# Rename solution file
$oldSlnPath = ".\BaseTemplate.sln"
$newSlnFileName = "$projectName.sln"
if (Test-Path $oldSlnPath) {
    Rename-Item -Path $oldSlnPath -NewName $newSlnFileName -Force
    Write-Output "Renamed solution: BaseTemplate.sln → $newSlnFileName"
}

# Replace namespace in all .cs files
Write-Output "Updating namespaces..."
Find-And-Replace-Recursive -rootPath "." -oldText "BaseTemplate" -newText $projectName

# Update project references in .sln file
Write-Output "Updating solution file references..."
$slnPath = ".\$newSlnFileName"
$slnContent = Get-Content $slnPath -Raw
$slnContent = $slnContent -replace "BaseTemplate", $projectName
Set-Content -Path $slnPath -Value $slnContent

# Update project references in .csproj files
Write-Output "Updating project file references..."
Get-ChildItem -Path "." -Filter "*.csproj" -Recurse | ForEach-Object {
    $csprojContent = Get-Content $_.FullName -Raw
    $csprojContent = $csprojContent -replace "BaseTemplate", $projectName
    Set-Content -Path $_.FullName -Value $csprojContent
    Write-Output "Updated: $($_.FullName)"
}

Write-Output "Template rename completed successfully!"
Write-Output "Building solution to verify..."

try {
    & dotnet build $newSlnFileName -c Release
    Write-Output "Build successful!"
}
catch {
    Write-Error "Build failed: $_"
    exit 1
}