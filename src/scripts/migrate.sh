<<<<<<< HEAD
export ASPNETCORE_ENVIRONMENT=Production

migrate_tweet() {
    local name="$1"
    local proj="$2"

#    dotnet ef migrations add "$name" \
#	--project $proj/Infrastructure \
#	--startup-project $proj/Api \
#	--output-dir Persistence/Migrations

    dotnet ef migrations add "$name" \
	--project Services/Tweet/Api/Tweet.Infrastructure \
	--startup-project Services/Tweet/Api/Tweet.Api \
	--output-dir Persistence/Migrations
}

migrate_identity() {
    dotnet ef migrations add "$1" \
	--context ApplicationDbContext \
	--project Services/Identity/Identity.Api \
	--startup-project Services/Identity/Identity.Api \
	--output-dir Infrastructure/Persistence/Migrations \
	# --configuration Release

#    dotnet ef migrations add "$1" \
#	--context ConfigurationDbContext \
#	--project Services/Identity/Identity.Api \
#	--startup-project Services/Identity/Identity.Api \
#	--output-dir Infrastructure/Persistence/Migrations/ConfigurationDb \
#	# --configuration Release

}

# migrate_identity "init-migration"
# migrate_tweet "init-migration"

||||||| 40a427a
=======
export ASPNETCORE_ENVIRONMENT=Production

migrate_tweet() {
    local name="$1"
    local proj="$2"

#    dotnet ef migrations add "$name" \
#	--project $proj/Infrastructure \
#	--startup-project $proj/Api \
#	--output-dir Persistence/Migrations

    dotnet ef migrations add "$name" \
	--project Services/Tweet/Api/Tweet.Infrastructure \
	--startup-project Services/Tweet/Api/Tweet.Api \
	--output-dir Persistence/Migrations
}

migrate_identity() {
    dotnet ef migrations add "$1" \
	--context PersistedGrantDbContext  \
	--project Services/Identity/Identity.Api \
	--startup-project Services/Identity/Identity.Api \
	--output-dir Infrastructure/Persistence/Migrations/PersistedGrantDb

    dotnet ef migrations add "$1" \
	--context ConfigurationDbContext  \
	--project Services/Identity/Identity.Api \
	--startup-project Services/Identity/Identity.Api \
	--output-dir Infrastructure/Persistence/Migrations/ConfigurationDb

    dotnet ef migrations add "$1" \
	--context ApplicationDbContext \
	--project Services/Identity/Identity.Api \
	--startup-project Services/Identity/Identity.Api \
	--output-dir Infrastructure/Persistence/Migrations
}

# migrate_identity "init-migration"
# migrate_tweet "init-migration"

>>>>>>> main
