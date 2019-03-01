variable "prefix" {
  description = "Choose a personal prefix (1-10 chars) that is attached to every resource to ensure a unique name."
}

variable "resource_name" {
  default     = "contoso"
  description = "Name of the project, which will be attached to every resource name."
}
