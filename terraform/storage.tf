# Simple Storage Service
resource "aws_s3_bucket" "customcads_bucket" {
  bucket = "customcads"
}

# Elastic Container Registry
resource "aws_ecr_repository" "customcads_container_registry" {
  name = "ninjatabg/customcads"
  encryption_configuration {
    encryption_type = "KMS"
  }
}
