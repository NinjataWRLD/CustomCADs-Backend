# Files Bucket
resource "aws_s3_bucket" "customcads_bucket" {
  bucket = "customcads-bucket"
}

# Versions Bucket
resource "aws_s3_bucket" "customcads_versions" {
  bucket = "customcads-versions"
}

# Latest ZIP
resource "aws_s3_object" "customcads_zip" {
  bucket     = aws_s3_bucket.customcads_versions.bucket
  key        = "app.zip"
  source     = "../App.zip"
  depends_on = [aws_s3_bucket.customcads_versions]
}

# Terraform Bucket
resource "aws_s3_bucket" "customcads_terraform" {
  bucket = "customcads-terraform"
}

# Terraform State
resource "aws_s3_object" "customcads_terraform_backup_state" {
  bucket     = aws_s3_bucket.customcads_terraform.bucket
  key        = "terraform.tfstate.backup"
  source     = "terraform.tfstate.backup"
  depends_on = [aws_s3_bucket.customcads_terraform]
}

# Container Registry
resource "aws_ecr_repository" "customcads_container_registry" {
  name = "ninjatabg/customcads"
  encryption_configuration {
    encryption_type = "KMS"
  }
}
