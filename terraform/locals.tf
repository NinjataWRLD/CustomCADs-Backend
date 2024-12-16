locals {
  env_vars = jsondecode(data.aws_secretsmanager_secret_version.customcads_env_variables_version.secret_string)

  # Extract groups
  jwt      = local.env_vars["JwtSettings"]
  payment  = local.env_vars["Payment"]
  email    = local.env_vars["Email"]
  storage  = local.env_vars["Storage"]
  delivery = local.env_vars["Delivery"]
  urls     = local.env_vars["URLs"]
}
