# 🚀 RabbitMQ & MassTransit MVP (Event-Driven Architecture)

Bu proje, .NET 9 kullanarak **Event-Driven Architecture (EDA)** prensiplerini uygulamak amacıyla geliştirilmiş bir *MVP* çalışmasıdır. Sistem, asenkron mesajlaşma kullanarak servisler arası iletişimi (Loose Coupling) ve ölçeklenebilirliği hedefler.



## 🛠 Kullanılan Teknolojiler
*   **.NET 9** (Web API & Background Services)
*   **MassTransit** (Message Bus Abstraction)
*   **RabbitMQ** (Message Broker)
*   **Docker & Docker Compose** (Containerization)

## 🏗 Mimari Yapı
Sistem üç ana bileşenden oluşmaktadır:

1.  **Producer (API):** Dış dünyadan (Postman/UI) gelen sipariş isteklerini karşılar ve bu isteği bir "Event" (OrderCreatedEvent) olarak RabbitMQ üzerine fırlatır.
2.  **Message Broker (RabbitMQ):** Producer'dan gelen mesajları kuyrukta (`order-queue`) saklar ve ilgili Consumer'lara iletir.
3.  **Consumer (Console/Worker):** Kuyruğu dinler, yeni bir mesaj geldiğinde asenkron olarak yakalar ve işleme alır (Loglama, Email gönderimi simülasyonu vb.).

## ⚙️ Kurulum ve Çalıştırma

Projeyi yerel makinenizde çalıştırmak için Docker yüklü olması yeterlidir.

1.  **Repoyu klonlayın:**
    ```bash
    git clone [https://github.com/SXTP/distributed-event-bus-sample.git]
    cd distributed-event-bus-sample

2. **Docker Compose ile ayağa kaldırın:**
  ```bash
    docker-compose up --build
   ```

3.  **Servislerin Durumu:**
    *   **API:** `http://localhost:5000`
    *   **RabbitMQ Management:** `http://localhost:15672` (User: `guest`, Pass: `guest`)

## 🚀 Test Etme

Postman veya curl üzerinden aşağıdaki adrese bir **POST** isteği göndererek sistemi test edebilirsiniz:

**Endpoint:** `POST http://localhost:5000/api/order`

**Request Body:**
```json
{
    "orderId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "price": 1250.50
}
```

**Sonuç:** 
İsteği gönderdikten sonra **Consumer** terminal ekranında mesajın başarıyla işlendiğini (Console Log) ve **RabbitMQ Management** panelinde trafik akışını anlık olarak görebilirsiniz.

## 📈 Ölçeklenebilirlik (Scaling)
Bu mimari, **Competing Consumers** deseni sayesinde yatayda kolayca ölçeklenebilir. Yoğun yük altında aşağıdaki komutla birden fazla consumer instance'ı ayağa kaldırabilirsiniz:

```bash
docker-compose up --scale consumer.console=3
```

## 📝 Notlar
*   Projede **MassTransit** kullanılarak servisler arası mesajlaşma stabilitesi sağlanmıştır.
*   Container'lar arası iletişim, Docker Compose üzerinde tanımlanan izole bir **sanal network** üzerinden güvenli bir şekilde gerçekleştirilmektedir.
*   Sistem, .NET 9'un güncel özellikleri ve MassTransit'in modern yapılandırma standartları ile uyumlu hale getirilmiştir.
