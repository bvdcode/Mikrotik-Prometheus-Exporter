# Mikrotik-Prometheus-Exporter
A minimalistic, no-bullshit Prometheus exporter that grabs RX/TX byte counters from MikroTik routers directly via the RouterOS API. No SNMP. No YAML hell. No over-engineered configs. No sysadmin voodoo.

```yaml
environment:
  - MIKROTIK_HOST=192.168.88.1
  - MIKROTIK_PORT=8728
  - MIKROTIK_USERNAME=admin
  - MIKROTIK_PASSWORD=passw0rd
```