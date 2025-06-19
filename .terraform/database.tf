# Production Database
resource "aws_db_instance" "customcads_production_database" {
  identifier     = "customcads-production-db"
  db_name        = "CustomCADs_Production"
  instance_class = "db.t3.micro"

  engine         = "postgres"
  engine_version = "16.8"
  port           = 5432

  username = local.production_db_username
  password = local.production_db_password

  vpc_security_group_ids = [aws_security_group.customcads_db_security_group.id]
  db_subnet_group_name   = aws_db_subnet_group.customcads_subnet_group.name

  storage_type          = "gp2"
  allocated_storage     = 20
  max_allocated_storage = 1000
  storage_encrypted     = true

  multi_az                   = false
  auto_minor_version_upgrade = true
  maintenance_window         = "Sun:09:00-Sun:09:30"
  skip_final_snapshot        = true
  parameter_group_name       = "default.postgres16"
}

# Staging Database
resource "aws_db_instance" "customcads_staging_database" {
  identifier     = "customcads-staging-db"
  db_name        = "CustomCADs_Staging"
  instance_class = "db.t3.micro"

  engine         = "postgres"
  engine_version = "16.8"
  port           = 5432

  username = local.staging_db_username
  password = local.staging_db_password

  vpc_security_group_ids = [aws_security_group.customcads_db_security_group.id]
  db_subnet_group_name   = aws_db_subnet_group.customcads_subnet_group.name

  storage_type          = "gp2"
  allocated_storage     = 20
  max_allocated_storage = 1000
  storage_encrypted     = true

  multi_az                   = false
  auto_minor_version_upgrade = true
  maintenance_window         = "Sun:08:00-Sun:08:30"
  skip_final_snapshot        = true
  parameter_group_name       = "default.postgres16"
}
