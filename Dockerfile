# Set the base image as the .NET 6.0 SDK (this includes the runtime)
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env

# Copy everything and publish the release (publish implicitly restores and builds)
COPY . ./
RUN dotnet publish ./Appegy.GitHub.UnityVersionAction/Appegy.GitHub.UnityVersionAction.csproj -c Release -o out --no-self-contained

# Label the container
LABEL maintainer="Ivan Murashka <imurashka.me@gmail.com>"
LABEL repository="https://github.com/appegy/unity-version-action"
LABEL homepage="https://github.com/appegy/unity-version-action"

# Label as GitHub action
LABEL com.github.actions.name="Find Unity version and changeset"
LABEL com.github.actions.description="Returns Unity version and changeset based on project path"
LABEL com.github.actions.icon="box"
LABEL com.github.actions.color="gray-dark"

# Relayer the .NET SDK, anew with the build output
FROM mcr.microsoft.com/dotnet/sdk:6.0
COPY --from=build-env /out .
ENTRYPOINT [ "dotnet", "/Appegy.GitHub.UnityVersionAction.dll" ]
