#######################################################
# Step 1: Build the application in a container        #
#######################################################
# Download the official ASP.NET Core SDK image 
# to build the project while creating the docker image
FROM microsoft/dotnet:2.1-sdk-alpine as build
WORKDIR /app

# Restore NuGet packages
COPY *.csproj .
RUN dotnet restore

# Copy the rest of the files over
COPY . .

# Build the application
RUN dotnet publish --output /out/ --configuration Debug

#######################################################
# Step 2: Run the build outcome in a container        #
#######################################################
# Download the official ASP.NET Core Runtime image
# to run the compiled application
FROM microsoft/dotnet:2.1.3-aspnetcore-runtime-alpine

WORKDIR /app

# Open Bot port
EXPOSE 3978

# Copy the build output from the SDK image
COPY --from=build /out .

# Start the application
ENTRYPOINT ["dotnet", "ContosoMaintenance.Bot.dll"] 