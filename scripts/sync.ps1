$from = "C:\Code\payaware-qa\testing"

$to = "C:\Code\Selenium.Framework"


Get-ChildItem -Path $from -Filter *Selenium.* | % {
    write-host $_.FullName
    $projectName = $_.Name

    $source = $_.FullName
    $destination = "$to\$projectName"

    robocopy $source $destination /mir 

}