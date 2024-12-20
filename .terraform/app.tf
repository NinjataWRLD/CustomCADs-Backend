# Application
resource "aws_elastic_beanstalk_application" "customcads_app" {
  name        = "CustomCADs"
  description = "The Application for CustomCADs"
}

# Version
resource "aws_elastic_beanstalk_application_version" "customcads_app_version" {
  application = aws_elastic_beanstalk_application.customcads_app.name
  bucket      = aws_s3_bucket.customcads_versions.bucket
  key         = aws_s3_object.customcads_zip.key
  name        = "latest"
  depends_on  = [aws_elastic_beanstalk_application.customcads_app]
}

# Key pair
resource "aws_key_pair" "customcads_key_pair" {
  key_name   = "customcads-key-pair"
  public_key = file("CustomCADs.pub")
}
