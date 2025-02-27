# Container Registry
resource "aws_ecr_repository" "customcads_container_registry" {
  name = "ninjatabg/customcads"
  encryption_configuration {
    encryption_type = "KMS"
  }
}

# Development Files Bucket
resource "aws_s3_bucket" "customcads_development_bucket" {
  bucket = "customcads-development-bucket"
}
resource "aws_s3_bucket_cors_configuration" "customcads_development_bucket_cors" {
  bucket = aws_s3_bucket.customcads_development_bucket.id

  cors_rule {
    allowed_headers = ["*"]
    allowed_methods = ["HEAD", "GET", "PUT", "POST", "DELETE"]
    allowed_origins = ["https://localhost:7295", "https://localhost:5173", "https://localhost:5174", "http://localhost:4173"]
    expose_headers  = ["ETag", "x-amz-id-2", "x-amz-request-id", "x-amz-meta-file-name"]
    max_age_seconds = 3000
  }
}

# Staging Files Bucket
resource "aws_s3_bucket" "customcads_staging_bucket" {
  bucket = "customcads-staging-bucket"
}
resource "aws_s3_bucket_cors_configuration" "customcads_staging_bucket_cors" {
  bucket = aws_s3_bucket.customcads_staging_bucket.id

  cors_rule {
    allowed_headers = ["*"]
    allowed_methods = ["HEAD", "GET", "PUT", "POST", "DELETE"]
    allowed_origins = ["https://main.d1apebjda2nmov.amplifyapp.com", "https://customcads.com", "https://www.customcads.com"]
    expose_headers  = ["ETag", "x-amz-id-2", "x-amz-request-id", "x-amz-meta-file-name"]
    max_age_seconds = 3000
  }
}

# Production Files Bucket
resource "aws_s3_bucket" "customcads_production_bucket" {
  bucket = "customcads-production-bucket"
}
resource "aws_s3_bucket_cors_configuration" "customcads_production_bucket_cors" {
  bucket = aws_s3_bucket.customcads_production_bucket.id

  cors_rule {
    allowed_headers = ["*"]
    allowed_methods = ["HEAD", "GET", "PUT", "POST", "DELETE"]
    allowed_origins = ["https://main.d1apebjda2nmov.amplifyapp.com", "https://customcads.com", "https://www.customcads.com"]
    expose_headers  = ["ETag", "x-amz-id-2", "x-amz-request-id", "x-amz-meta-file-name"]
    max_age_seconds = 3000
  }
}

# Versions Bucket
resource "aws_s3_bucket" "customcads_versions" {
  bucket = "customcads-versions"
}
# Latest ZIP Object
resource "aws_s3_object" "customcads_zip" {
  bucket     = aws_s3_bucket.customcads_versions.bucket
  key        = "app.zip"
  source     = "../App.zip"
  depends_on = [aws_s3_bucket.customcads_versions]
}
