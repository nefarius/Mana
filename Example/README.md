# Example logging stack for Docker

Lightweight Docker Logging stack

```yaml
    logging:
      driver: "fluentd"
      options:
        fluentd-address: 127.0.0.1:24224
        fluentd-sub-second-precision: true
        tag: container-name
```
