# Production 

## Database Credentials
data "aws_secretsmanager_secret" "customcads_production_database_password" {
  name = "customcads-production/db-credentials"
}
data "aws_secretsmanager_secret_version" "customcads_production_database_password_version" {
  secret_id = data.aws_secretsmanager_secret.customcads_production_database_password.id
}
## Environmental Variables
data "aws_secretsmanager_secret" "customcads_production_env_variables" {
  name = "customcads-production/env-variables"
}
data "aws_secretsmanager_secret_version" "customcads_production_env_variables_version" {
  secret_id = data.aws_secretsmanager_secret.customcads_production_env_variables.id
}

# Staging

## Database Credentials
data "aws_secretsmanager_secret" "customcads_staging_database_password" {
  name = "customcads-staging/db-credentials"
}
data "aws_secretsmanager_secret_version" "customcads_staging_database_password_version" {
  secret_id = data.aws_secretsmanager_secret.customcads_staging_database_password.id
}
## Environmental Variables
data "aws_secretsmanager_secret" "customcads_staging_env_variables" {
  name = "customcads-staging/env-variables"
}
data "aws_secretsmanager_secret_version" "customcads_staging_env_variables_version" {
  secret_id = data.aws_secretsmanager_secret.customcads_staging_env_variables.id
}
