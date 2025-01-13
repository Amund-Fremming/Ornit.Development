# Auth0 Setup guide

## Steps
1. Navigate to Auth0 Dashboard
2. Create a new Api under Application > APIs > Create API
3. Add refresh tokens and password under Applications > Your app > Settings > Advanced Settings > Grant Types > Save Changes
4. Add configuration values from Applications > Your app > Settings like so
```json
"Auth0": {
    "Issuer": "https://<your_domain>",
    "Audience": "<audience_Indentifier>",
    "ClientId": "<your_client-id>",
    "ClientSecret": "your_client-secret>"
}
```
5. Add a default directory to "Username-Password-Authentication"  ___ under Settings > General > Default Directory
