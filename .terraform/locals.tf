locals {
  production_db_credentials = jsondecode(data.aws_secretsmanager_secret_version.customcads_production_database_password_version.secret_string)
  production_db_username    = local.production_db_credentials["username"]
  production_db_password    = local.production_db_credentials["password"]

  staging_db_credentials = jsondecode(data.aws_secretsmanager_secret_version.customcads_staging_database_password_version.secret_string)
  staging_db_username    = local.staging_db_credentials["username"]
  staging_db_password    = local.staging_db_credentials["password"]
}