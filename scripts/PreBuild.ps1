
<#
#̷𝓍   𝓐𝓡𝓢 𝓢𝓒𝓡𝓘𝓟𝓣𝓤𝓜
#̷𝓍   🇵​​​​​🇴​​​​​🇼​​​​​🇪​​​​​🇷​​​​​🇸​​​​​🇭​​​​​🇪​​​​​🇱​​​​​🇱​​​​​ 🇸​​​​​🇨​​​​​🇷​​​​​🇮​​​​​🇵​​​​​🇹​​​​​ 🇧​​​​​🇾​​​​​ 🇬​​​​​🇺​​​​​🇮​​​​​🇱​​​​​🇱​​​​​🇦​​​​​🇺​​​​​🇲​​​​​🇪​​​​​🇵​​​​​🇱​​​​​🇦​​​​​🇳​​​​​🇹​​​​​🇪​​​​​.🇶​​​​​🇨​​​​​@🇬​​​​​🇲​​​​​🇦​​​​​🇮​​​​​🇱​​​​​.🇨​​​​​🇴​​​​​🇲​​​​​
#>

[CmdletBinding(SupportsShouldProcess)]
param()


function Get-RootDirectory {
    Split-Path -Parent (Split-Path -Parent $PSCommandPath)
}

function Get-ScriptDirectory {
    Split-Path -Parent $PSCommandPath
}

$Script:ScriptsDirectory = Get-ScriptDirectory
$Script:RootDirectory = Get-RootDirectory
$Script:DllDirectory = Join-Path $Script:RootDirectory $Path
$Script:ToolsDirectory = Join-Path $Script:RootDirectory "tools"
$Script:TestDirectory = Join-Path $Script:RootDirectory "test"
$Script:TestLoadingDirectories = Join-Path $Script:TestDirectory "loading"
$Script:PkPath = Join-Path $Script:ToolsDirectory "pk.exe"

$pkExe = $Script:PkPath

&"$pkExe" "pwsh"

Remove-Item "$Script:TestLoadingDirectories" -Recurse -Force -ErrorAction Ignore | Out-Null

exit 0