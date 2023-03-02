# dotnet-clean-architecture-graphql

## Introduction

This project is an example implementation of Clean Architecture principles using GraphQL. The goal is to demonstrate how Clean Architecture can be used to create a scalable and maintainable application with a flexible and efficient GraphQL API.

## Key Design Decisions

### Create ObjectTypes via Code-First patterns

Use Hot Chocolate 'Code-First' patterns to provide a wrapper around the domain objects and avoid exposing DB objects directly to the consumer.  The alternative would be to use 'Annotation-based' patterns, but that would involve decorating our domain objects with GraphQL-related attributes.