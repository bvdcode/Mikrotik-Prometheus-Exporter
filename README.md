# Mikrotik Prometheus Exporter

A minimalistic Prometheus exporter that grabs RX/TX byte counters from MikroTik routers directly via the RouterOS API.

This exporter is designed to make it easier to monitor the traffic and status of MikroTik network interfaces with Prometheus without the pain of dealing with overly complex configurations or non-standard protocols.

---

## Features

- **Simple Prometheus integration** with **MikroTik RouterOS API**.
- **No SNMP** involved, completely based on the RouterOS API.
- **Minimal setup**: Just set the MikroTik host, port, username, and password.
- **Collects key metrics** like RX/TX bytes, interface status, and more.
- Easily extensible for additional metrics.

---

## Configuration

To configure the exporter, use environment variables for the following settings:

```yaml
environment:
  - MIKROTIK_HOST=192.168.88.1 # IP address of your MikroTik router
  - MIKROTIK_PORT=8728 # API port (default: 8728, non-SSL)
  - MIKROTIK_USERNAME=admin # Username for MikroTik API login
  - MIKROTIK_PASSWORD=passw0rd # Password for MikroTik API login
```

These values can be set as environment variables or passed in your Docker setup (if using Docker).

---

## Installation

You can build the Docker image for this exporter or run it directly from a pre-built image.

### Using local Docker

1. Clone the repository:

   ```bash
   git clone https://github.com/yourusername/Mikrotik-Prometheus-Exporter.git
   cd Mikrotik-Prometheus-Exporter
   ```

2. Build the Docker image:

   ```bash
   docker build -t mikrotik-prometheus-exporter .
   ```

3. Run the Docker container with the necessary environment variables:

   ```bash
   docker run -e MIKROTIK_HOST=192.168.88.1 -e MIKROTIK_PORT=8728 -e MIKROTIK_USERNAME=admin -e MIKROTIK_PASSWORD=passw0rd -p 8080:8080 mikrotik-prometheus-exporter
   ```

### Using Docker Compose

Alternatively, you can use Docker Compose to quickly deploy the exporter. Here's a sample `docker-compose.yml`:

```yaml
services:
  mikrotik-exporter:
    image: bvdcode/mikrotik-exporter:v1.0.0
    environment:
      - MIKROTIK_HOST=192.168.88.1
      - MIKROTIK_PORT=8728
      - MIKROTIK_USERNAME=admin
      - MIKROTIK_PASSWORD=passw0rd
    ports:
      - "8080:8080" # Or ignore if using prometheus in the same docker network
    restart: unless-stopped
```

Then run:

```bash
docker-compose up -d
```

---

## Metrics

The exporter exposes the following metrics on the `/metrics` endpoint:

- **mikrotik_rx_bytes**: Total received bytes per interface.
- **mikrotik_tx_bytes**: Total transmitted bytes per interface.
- **mikrotik_interface_status**: Interface status (1 for active, 0 for inactive).
- **mikrotik_interface_type**: The type of interface (e.g., `ether`, `vrrp`, etc.).

Example metrics output:

```plaintext
# HELP mikrotik_rx_bytes Total received bytes per interface
# TYPE mikrotik_rx_bytes counter
mikrotik_rx_bytes{iface="ether1"} 123456789

# HELP mikrotik_tx_bytes Total transmitted bytes per interface
# TYPE mikrotik_tx_bytes counter
mikrotik_tx_bytes{iface="ether1"} 987654321
```

---

## Usage with Prometheus

To start collecting metrics, add the exporter as a target in your Prometheus configuration:

```yaml
scrape_configs:
  - job_name: "mikrotik"
    static_configs:
      - targets: ["<your_exporter_host>:8080"]
```

---

## License

This project is licensed under the MIT License — see the [LICENSE](LICENSE) file for details.

---

### **Conclusion**

This exporter simplifies the process of pulling important metrics from your MikroTik routers directly into Prometheus without the complexity of SNMP or unnecessary configurations. It's quick to set up and can be easily extended for additional metrics as your monitoring needs grow.

Feel free to contribute or fork the repo for your own needs!

---
