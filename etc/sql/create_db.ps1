
# Define parameters
$serverName = "(LocalDB)\MSSQLLocalDB"
$sqlScriptPath = "JokeDb.sql"  # Path to your SQL script file

# Use Windows Authentication
$sqlcmdCommand = "sqlcmd -S `"$serverName`" -i `"$sqlScriptPath`" -E"

# Execute the command
Invoke-Expression $sqlcmdCommand
