locals {
  db_credentials = jsondecode(data.aws_secretsmanager_secret_version.customcads_database_password_version.secret_string)
  db_username    = local.db_credentials["username"]
  db_password    = local.db_credentials["password"]

  env_vars = jsondecode(data.aws_secretsmanager_secret_version.customcads_env_variables_version.secret_string)
  jwt      = local.env_vars["JwtSettings"]
  payment  = local.env_vars["Payment"]
  email    = local.env_vars["Email"]
  storage  = local.env_vars["Storage"]
  delivery = local.env_vars["Delivery"]
  urls     = local.env_vars["URLs"]
}
