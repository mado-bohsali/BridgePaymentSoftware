Scenario 1:

Google search queries:
"how to abstract operations using API"
"payment gateways working with GoLang"
"How to compose a technology stack"
"What is the workflow of an ecommerce transaction"
"What databse systems work with GoLang"
"blockchain payment gateway stack for golang"

Proposed technology stacks:
Stack 1:

The idiomatic way to use a SQL, or SQL-like, database in Go is through the database/sql package. It provides a lightweight interface to a row-oriented database.

As for the web service, resources form the nucleus of any REST API design. Resource identifiers (URI), Resource representations, API operations (using various HTTP methods), etc. are all built around the concept of Resources. It is very important to select the right resources and model the resources at the right granularity while designing the REST API so that the API consumers get the desired functionality from the APIs, the APIs behave correctly and the APIs are maintainable.

Once the nouns (resources) have been identified, then the interactions with the API can be modeled as HTTP verbs against these nouns. When they don't map nicely, we could approximate. For example, we can easily use the “nouns in the domain” approach and identify low level resources such as Post, Tag, Comment, etc. in a blogging domain. Similarly, we can identify the nouns Customer, Address, Account, Teller, etc. as resources in a banking domain. As for authorization, the bank handles it by returning the JWT back to the merchant at the client side.

Stack2:

As for an alternative full node bitcoin implementation, it properly relays newly mined blocks, maintains a transaction pool, and relays individual transactions that have not yet made it into a block. It ensures all individual transactions admitted to the pool follow the rules required by the block chain and also includes more strict checks which filter transactions based on miner requirements ("standard" transactions).

Stacks URL: https://drive.google.com/file/d/1AjFelcgdltAy_4om7Qjs-vZmjvXpm6nX/view?usp=sharing

######################################
Scenario 2:

Google search queries:
"GoLang scalability"
"how to build radio application"
"GoLang features"
"streaming services with Golang"

Proposed technology stacks:
Stack 1:

The server will get audio input from any available audio input on the server, like your Macbook’s microphone if you’re running the server on a Macbook, convert it to binary, and send out a chunked response to the client. Chunking the response in http is a way of sending out partials of data, or “chunks” of data, which is especially handy if you have a data set that is not complete yet. The library used is called Portaudio. It will handle all audio regardless of the server’s OS (OSX/Windows/Linux). Portaudio works with a main audio loop where you will do something with the audio input and output.


Stack2:

URL: https://drive.google.com/file/d/1jWkfvjS-SdhxbQ7XJDTJ05eYEoW2-Jgg/view?usp=sharing

######################################
Scenario 3:

Google search queries:
"what is jwt token"
"benefits of RESTful architecture"
""
""

Proposed technology stacks:
Stack 1:

The application or client requests authorization to the authorization server. This is performed through one of the different authorization flows. For example, a typical OpenID Connect compliant web application will go through the /oauth/authorize endpoint using the authorization code flow.
When the authorization is granted, the authorization server returns an access token to the application.
The application uses the access token to access a protected resource (like an API).

The outputs of authorization are three Base64-URL strings separated by dots that can be easily passed in HTML and HTTP environments, while being more compact when compared to XML-based standards such as SAML. The payload, header and signature all contribute to authorizing users.

Do note that with signed tokens, all the information contained within the token is exposed to users or other parties, even though they are unable to change it. This means you should not put secret information within the token.

As JSON is less verbose than XML, when it is encoded its size is also smaller, making JWT more compact than SAML. This makes JWT a good choice to be passed in HTML and HTTP environments. JSON parsers are common in most programming languages because they map directly to objects. Conversely, XML doesn't have a natural document-to-object mapping. This makes it easier to work with JWT than SAML assertions.

Regarding usage, JWT is used at Internet scale. This highlights the ease of client-side processing of the JSON Web token on multiple platforms, especially mobile.


Stack2:

URL: https://drive.google.com/file/d/1S5I3LCTrYKVyxAQ2E_Cg8fNFVD_FIW67/view?usp=sharing
