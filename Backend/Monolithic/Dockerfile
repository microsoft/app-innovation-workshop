#######################################################
# Step 1: Build the application in a container        #
#######################################################

# Download the official ASP.NET Core SDK image 
# to build the project while creating the docker image
FROM microsoft/dotnet:2.1-sdk as build
WORKDIR /app

# Restore NuGet packages
COPY *.csproj .
RUN dotnet restore

# Copy the rest of the files over
COPY . .

# Build the application
RUN dotnet publish --output /out/ --configuration Release

#######################################################
# Step 2: Run the build outcome in a container        #
#######################################################

# Download the official ASP.NET Core Runtime image 
# to run the compiled application
FROM microsoft/dotnet:2.1.4-aspnetcore-runtime-alpine
#FROM microsoft/dotnet:2.1.4-aspnetcore-runtime-sac2016
WORKDIR /app

# Open HTTP and HTTPS ports
EXPOSE 80
EXPOSE 443

# Copy the build output from the SDK image
COPY --from=build /out .

# Start the application
ENTRYPOINT ["dotnet", "ContosoMaintenance.WebAPI.dll"]