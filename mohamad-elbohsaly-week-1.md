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

Once the nouns (resources) have been identified, then the interactions with the API can be modeled as HTTP verbs against these nouns. When they don't map nicely, we could approximate. For example, we can easily use the “nouns in the domain” approach and identify low level resources such as Post, Tag, Comment, etc. in a blogging domain. Similarly, we can identify the nouns Customer, Address, Account, Teller, etc. as resources in a banking domain.

As for authorization, the bank handles it by returning the JWT back to the merchant at the client side.

Stack2:

As for an alternative full node bitcoin implementation, it properly relays newly mined blocks, maintains a transaction pool, and relays individual transactions that have not yet made it into a block. It ensures all individual transactions admitted to the pool follow the rules required by the block chain and also includes more strict checks which filter transactions based on miner requirements ("standard" transactions).

######################################
Scenario 2:
Google search queries:

Proposed technology stacks:
Stack 1:


Stack2:

######################################
Scenario 3:
Google search queries:

Proposed technology stacks:
Stack 1:


Stack2:
