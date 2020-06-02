#vstest.console --?
$projectName = 'PayAware-QA'

$branchName = 'develop'

$modulesFolder = "C:\Code\PowerShell.Libs"
foreach ($module in Get-Childitem $modulesFolder -Name -Filter "*.psm1")
{
    Import-Module $modulesFolder\$module -Force
}

SetEvent 'auto'

#$repoUrl = 'git@bitbucket.org:zhuoruichen/payaware-qa.git'
#$localRepoPath = GetCodeFromGit $projectName $repoUrl $branchName
#$repoPath = $localRepoPath[0].FullName
$repoPath = "C:\Code\payaware-qa"

UpdateCode $repoPath $branchName

#$repoPath = "C:\Code\payaware"
$solutionPath = "$repoPath\testing\Testing.sln"
#$solutionPath = "C:\Code\payaware-qa\testing\Testing.sln"

$nuget = "$repoPath\tools\nuget.exe"
$msbuild = "C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe"

&$nuget restore $solutionPath
&$msbuild $solutionPath /t:Rebuild /p:Configuration=Release

$testFile = "$repoPath\testing\End2End.PayAware.Api\bin\Release\netcoreapp3.1\End2End.PayAware.Api.dll"

#download vs2017, only need install msbuild and test feature.
#$vstest = "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\Extensions\TestPlatform\vstest.console.exe"

$vstest = "C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\Common7\IDE\Extensions\TestPlatform\vstest.console.exe"

#dotnet test End2End.PayAware.Api.dll  --collect "Code Coverage" --results-directory .\TestResults


### run test
$timeStamp = Get-Date -Format "yyyyMMdd-HHmmss"
$projectName = (Get-Item $testFile).BaseName
#$runtimeSettings = (Get-Item $testFile).Directory.FullName+"\test.runsettings"
$runtimeSettings ="C:\Code\payaware-qa\testing\End2End.PayAware.Portal\test.runsettings"
$testReportPath = "C:\TestResult\"+$projectName+"_"+$timeStamp+"\"
$testCaseFilter = "End2End.PayAware.Api.TestSets"
&$vstest  $testFile /Settings:$runtimeSettings /Logger:trx /ResultsDirectory:$testReportPath /TestCaseFilter:$testCaseFilter


$trxer = "$repoPath\testing\Testing.Infrastructure\Tools\TrxerConsole.exe"

### generate report
Get-ChildItem -Path $testReportPath -Recurse -Include *.trx | % {
    $trxFile = $_.FullName
    &$trxer $trxFile
}

Get-ChildItem -Path $testReportPath -Recurse -Include *.html | % {
    $reportFile = $_.FullName
    $reportFile
}
