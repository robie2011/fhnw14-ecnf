$nuspecPath="C:\data\sourcecode\fhnw\ecnf\fhnw14-ecnf\RoutePlannerLib\RoutePlannerLib.nuspec"
$xml = [xml] (gc $nuspecPath)
$oldVersion = [double]$xml.package.metadata.version
$newVersion = ($oldVersion+0.1).ToString().replace(",",".")
$xml.package.metadata.version = $newVersion
$xml.Save($nuspecPath)

cd "C:\data\sourcecode\fhnw\ecnf\fhnw14-ecnf\RoutePlannerLib\"
nuget pack "C:\data\sourcecode\fhnw\ecnf\fhnw14-ecnf\RoutePlannerLib\RoutePlannerLib.csproj" -Symbols
nuget push "C:\data\sourcecode\fhnw\ecnf\fhnw14-ecnf\RoutePlannerLib\RoutPlanerLib.dll.$newVersion.nupkg" 582076ad-202b-4510-b27f-63fb09def35f -s https://staging.nuget.org