# Serets
data "aws_secretsmanager_secret" "customcads_database_password" {
  name = "customcads/db-credentials"
}
data "aws_secretsmanager_secret" "customcads_env_variables" {
  name = "customcads/env-varialbes"
}

# Versions
data "aws_secretsmanager_secret_version" "customcads_database_password_version" {
  secret_id = data.aws_secretsmanager_secret.customcads_database_password.id
}
data "aws_secretsmanager_secret_version" "customcads_env_variables_version" {
  secret_id = data.aws_secretsmanager_secret.customcads_env_variables.id
}
