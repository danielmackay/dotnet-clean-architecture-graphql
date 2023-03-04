# dotnet-clean-architecture-graphql

## Introduction

This project is an example implementation of Clean Architecture principles using GraphQL. The goal is to demonstrate how Clean Architecture can be used to create a scalable and maintainable application with a flexible and efficient GraphQL API.

## Features

- Queries
  - Paging
  - Sorting
  - Filtering
  - Projections
- Mutations
  - Create
  - Update
  - Delete
  - Fluent Validation

## Frontend

### Updating the GraphQL Schema

```ps1
dotnet graphql download https://localhost:44339/graphql/
```

## Key Design Decisions

### Create ObjectTypes via Code-First patterns

Use Hot Chocolate 'Code-First' patterns to provide a wrapper around the domain objects and avoid exposing DB objects directly to the consumer.  The alternative would be to use 'Annotation-based' patterns, but that would involve decorating our domain objects with GraphQL-related attributes.

### Return Unique Mutation Payloads

All mutations should return a unique payload object.  Generally, this will return the object being modified so that the user can decide which fields to return.  Using a unique object allows us to add future fields in future and not break the schema.

### Accept Unique Mutation Inputs

All mutations should accept a unique input object.  Using a unique object allows us to add future fields in future and not break the schema.
