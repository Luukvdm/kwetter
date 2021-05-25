export ASPNETCORE_ENVIRONMENT=Production

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

