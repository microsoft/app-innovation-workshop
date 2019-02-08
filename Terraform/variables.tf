variable "prefix" {
  description = "Choose a personal prefix (1-10 chars) that is attached to every resource to ensure a unique name."
}

variable "resource_name" {
  default     = "contoso"
  description = "Name of the project, which will be attached to every resource name."
}

variable "sp_client_id" {
  description = "Client ID of the Service Principal for AKS."
}

variable "sp_client_secret" {
  description = "Password of the Service Principal for AKS."
}
