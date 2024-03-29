﻿namespace Product.Api.Constants
{
    public class ServiceResponseMessages
    {
        public const string InvalidGuid = "The guid supplied was invalid";
        public const string ProductDoesNotExist = "The requested product does not exist";
        public const string UnexpectedError = "There was an unexpedted error.";
        public const string ProductSaveError = "The product save operation has failed";
        public const string ProductCreated = "Product successfully created";
        public const string ProductUpdated = "Product successfully updated";
    }
}
