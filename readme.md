# Simple Nancy Todo API
* Backed by a flakey dictionary data store. Definitely don't do this.
* Content Negotiation, but only tested with JSON (e.g. if you want a view, it's gonna break).
* Hosted in Docker using the enclosed dockerfile.
* Tests against the concrete type of the data store. Normally this would be faked out, but... it's a dictionary.
