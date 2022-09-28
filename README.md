<img src="assets/NSS-128x128.png" align="right" />

# Mana

A *tiny* work-in-progress [Kibana](https://www.elastic.co/kibana/) replacement using [Blazor](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor).

## Why

This project was created out of spite that every search for log harvesting and visualization on Docker resulted in recommending the ELK or EFK stack (**E**lastic, **F**luentd and **K**ibana) which is incredibly bloated and slow for most small setup of mine. I typically want to monitor small Docker Compose stacks with two to five containers in them, so the logging stack needs to be lightweight and fast. Neither of those attributes apply to Elasticsearch (slow startup, memory hungry), Fluentd (memory hungry) or Kibana (slow startup) so I crafted my own.

Instead of Elasticsearch I use [ZincSearch](https://zincsearch.com/), a fast, small Go application with Elastic-compatible HTTP API. Fluentd has been replaced with [Fluent Bit](https://fluentbit.io/). Last but not least I started to write my own little Kibana which currently only allows log live view and historic search. More to come, maybe, if I need it.

Look in the [example](./example) directory for a configuration template.

## 3rd party resources

- [elasticsearch + fluent-bit + kibana](https://github.com/qqbuby/efk-docker)
- [ZincSearch - fluent-bit](https://docs.zincsearch.com/ingestion/fluent-bit/)