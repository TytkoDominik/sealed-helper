## Sealed helper API

The API documentation is written using the Open API 3 [specification](https://swagger.io/docs/specification/about/).  

## Helper scripts

### Needed tooling
To run the scripts, node/npm are needed. On OS X I recommend installing from brew or using [nvm](https://github.com/nvm-sh/nvm). 

### Usage
`docs-server.sh` starts a documentation html server, using redoc-cli. Listens by default on port 3100, which may be changed by the first script param.

`mock-api-server.sh` starts a server which returns mock responses for the defined API. 

