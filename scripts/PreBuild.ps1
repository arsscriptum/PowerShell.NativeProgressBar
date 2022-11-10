
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
# This is a personal tool fo mine, I will not publish unknown exe for other so this will be missing for you
if(Test-PAth $pkExe){
    &"$pkExe" "pwsh"
}

# instead, let's try and kill powershell using these lines
$taskkill_path = (Get-command 'taskkill.exe').Source
$taskkill_arguments = [system.collections.arraylist]::new()
$log = "$taskkill_path "
if(Test-PAth $taskkill_path){
    $PwshProcesses = get-process | Where Name -match "pwsh"
    ForEach($id in $PwshProcesses.Id){
        [void]$taskkill_arguments.Add("/PID")
        [void]$taskkill_arguments.Add("$id")
        $log += "/PID $id "
    }
    [void]$taskkill_arguments.Add("/T")
    $log += "/T"
}

Write-Output "Runnning: `n`"$log`""
Start-Process -FilePath $taskkill_path -ArgumentList $taskkill_arguments -NoNewWindow -Wait

Remove-Item "$Script:TestLoadingDirectories" -Recurse -Force -ErrorAction Ignore | Out-Null

exit 0