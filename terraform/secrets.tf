data "aws_secretsmanager_secret" "customcads_database_password" {
  name = "rds!db-48a06954-6fe5-43dd-a6e2-c13f4a65fa4f"
}
data "aws_secretsmanager_secret" "customcads_env_variables" {
  name = "customcads/env-varialbes"
}

data "aws_secretsmanager_secret_version" "customcads_database_password_version" {
  secret_id = data.aws_secretsmanager_secret.customcads_database_password.id
}
data "aws_secretsmanager_secret_version" "customcads_env_variables_version" {
  secret_id = data.aws_secretsmanager_secret.customcads_env_variables.id
}
