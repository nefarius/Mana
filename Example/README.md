# Example logging stack for Docker

Adjust the credentials in the `docker-compose.yml` file for your needs, then add the following logger settings to your existing containers:

```yaml
    logging:
      driver: "fluentd"
      options:
        fluentd-address: 127.0.0.1:24224
        fluentd-sub-second-precision: true
        tag: container-name
```

This will rout the log outputs with nanosecond-precision timestamps through Fluent Bit into ZincSearch. From there it can be queried using either the Zinc Web UI or Mana.
