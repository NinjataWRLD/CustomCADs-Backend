locals {
  origin_id = "eb-customcads_app"
}

# Certificate Manager
resource "aws_acm_certificate" "customcads_certificate" {
  domain_name               = "customcads.com"
  subject_alternative_names = ["*.customcads.com", "*.api.customcads.com"]

  validation_method = "DNS"
  key_algorithm     = "EC_prime256v1"

  lifecycle {
    create_before_destroy = true
  }
}

# Cache policy
resource "aws_cloudfront_cache_policy" "customcads_cache_policy" {
  name        = "CustomCADsCachePolicy"
  comment     = "Cache policy for the CustomCADs API"
  default_ttl = 3600
  max_ttl     = 86400
  min_ttl     = 0

  parameters_in_cache_key_and_forwarded_to_origin {
    cookies_config {
      cookie_behavior = "all"
    }
    headers_config {
      header_behavior = "whitelist"
      headers {
        items = ["Origin", "Access-Control-Request-Headers", "Access-Control-Request-Method"]
      }
    }
    query_strings_config {
      query_string_behavior = "all"
    }
  }
}

# Production CloudFront Distribution
resource "aws_cloudfront_distribution" "customcads_production_cdn" {
  origin {
    domain_name = aws_elastic_beanstalk_environment.customcads_env_prod.cname
    origin_id   = local.origin_id
    origin_shield {
      enabled              = true
      origin_shield_region = var.region
    }

    custom_origin_config {
      http_port              = 80
      https_port             = 443
      origin_protocol_policy = "http-only"
      origin_ssl_protocols   = ["TLSv1.2"]
    }
  }

  enabled         = true
  is_ipv6_enabled = true
  aliases         = ["api.customcads.com"]

  default_cache_behavior {
    allowed_methods = ["DELETE", "GET", "HEAD", "OPTIONS", "PATCH", "POST", "PUT"]
    cached_methods  = ["GET", "HEAD"]

    target_origin_id       = local.origin_id
    cache_policy_id        = aws_cloudfront_cache_policy.customcads_cache_policy.id
    viewer_protocol_policy = "redirect-to-https"
  }

  price_class = "PriceClass_100"
  restrictions {
    geo_restriction {
      restriction_type = "whitelist"
      locations        = ["US", "CA", "GB", "DE", "BG", "RO"]
    }
  }

  viewer_certificate {
    cloudfront_default_certificate = false
    acm_certificate_arn            = aws_acm_certificate.customcads_certificate.arn
    ssl_support_method             = "sni-only"
    minimum_protocol_version       = "TLSv1.2_2018"
  }
}

# Staging CloudFront Distribution
resource "aws_cloudfront_distribution" "customcads_staging_cdn" {
  origin {
    domain_name = aws_elastic_beanstalk_environment.customcads_env_staging.cname
    origin_id   = local.origin_id
    origin_shield {
      enabled              = true
      origin_shield_region = var.region
    }

    custom_origin_config {
      http_port              = 80
      https_port             = 443
      origin_protocol_policy = "http-only"
      origin_ssl_protocols   = ["TLSv1.2"]
    }
  }

  enabled         = true
  is_ipv6_enabled = true
  aliases         = ["staging.api.customcads.com"]

  default_cache_behavior {
    allowed_methods = ["DELETE", "GET", "HEAD", "OPTIONS", "PATCH", "POST", "PUT"]
    cached_methods  = ["GET", "HEAD"]

    target_origin_id       = local.origin_id
    cache_policy_id        = aws_cloudfront_cache_policy.customcads_cache_policy.id
    viewer_protocol_policy = "redirect-to-https"
  }

  price_class = "PriceClass_100"
  restrictions {
    geo_restriction {
      restriction_type = "whitelist"
      locations        = ["US", "CA", "GB", "DE", "BG", "RO"]
    }
  }

  viewer_certificate {
    cloudfront_default_certificate = false
    acm_certificate_arn            = aws_acm_certificate.customcads_certificate.arn
    ssl_support_method             = "sni-only"
    minimum_protocol_version       = "TLSv1.2_2018"
  }
}
