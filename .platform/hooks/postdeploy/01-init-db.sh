set -e
echo "Running post-deploy database setup..."
dotnet ef database update
echo "Database setup complete."