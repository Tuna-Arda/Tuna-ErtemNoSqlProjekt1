# Backup-Skript für MongoDB mit mongodump
# Stelle sicher, dass mongodump im PATH verfügbar ist oder gib den vollständigen Pfad an

$mongoHost = "localhost"
$mongoPort = "27017"
$database = "JetstreamDB"
$outputDir = ".\Backup\$(Get-Date -Format 'yyyyMMdd_HHmmss')"

New-Item -ItemType Directory -Force -Path $outputDir

mongodump --host $mongoHost --port $mongoPort --db $database --username MyMigrationUser --password "<password>" --out $outputDir

Write-Host "Backup abgeschlossen. Dateien gespeichert in $outputDir"
