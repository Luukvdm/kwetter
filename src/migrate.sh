export ASPNETCORE_ENVIRONMENT=Production

migrate_project() {
    local name="$1"
    local proj="$2"

    dotnet ef migrations add "$name" \
	--project $proj/Infrastructure \
	--startup-project $proj/Api \
	--output-dir Persistence/Migrations    
}

migrate_identity() {
    dotnet ef migrations add "$0" \
	--context ApplicationDbContext \
	--project Services/Identity/Identity.Api \
	--startup-project Services/Identity/Identity.Api \
	--output-dir Infrastructure/Persistence/Migrations \
	# --configuration Release

    dotnet ef migrations add "$0" \
	--context ConfigurationDbContext \
	--project Services/Identity/Identity.Api \
	--startup-project Services/Identity/Identity.Api \
	--output-dir Infrastructure/Persistence/Migrations/ConfigurationDb \
	# --configuration Release

}

migrate_identity "Init migration"

