param([String] $XMLFile)

[XML]$projRoot = Get-Content $XMLfile

Write-Host $projRoot.SelectNodes('/*/*').Version