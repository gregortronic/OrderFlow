# OrderFlow

**OrderFlow** — backend-проект для обработки заказов, резервирования остатков и асинхронной обработки документов.

Проект моделирует production-like B2B-сценарий: заказ можно создать вручную или получить из документа, затем провалидировать, обработать в фоне и зарезервировать товар.

> **Status:** in development  
> **Current stage:** foundation

## Что делает система
- создание и просмотр заказов;
- работа со статусами заказа;
- загрузка документов на обработку;
- асинхронный pipeline обработки документов;
- резервирование и снятие резервов по складу;
- retry / idempotency / reliability-паттерны по мере развития проекта.

## Стек
**ASP.NET Core, EF Core, PostgreSQL, Worker Service**  
Дальше по roadmap: **RabbitMQ, Redis, Docker Compose, Health Checks, Integration Tests, Outbox**

## Архитектура
`Api / Application / Domain / Infrastructure / Worker`

## Roadmap

### 1. Foundation
- [x] Solution и базовая структура слоёв
- [x] Domain entities и enum-статусы
- [x] EF Core + PostgreSQL
- [x] Initial migration
- [ ] Orders API
- [ ] Documents API
- [ ] DTO + базовая валидация
- [ ] Пагинация / фильтрация / сортировка
- [ ] Global exception handling
- [ ] Structured logging

### 2. Async processing
- [ ] RabbitMQ publisher
- [ ] Worker consumer
- [ ] DocumentJob pipeline
- [ ] CSV parsing
- [ ] Создание заказа из документа
- [ ] Inventory reservation flow
- [ ] Retry for background processing

### 3. Reliability
- [ ] Redis cache-aside
- [ ] Cache invalidation
- [ ] Idempotent consumer
- [ ] Order cancel flow
- [ ] Optimistic concurrency for inventory
- [ ] Correlation ID
- [ ] Health checks
- [ ] Audit / status history
- [ ] Integration tests

### 4. Final Polish
- [ ] Dockerfile for API and Worker
- [ ] Docker Compose
- [ ] Admin / reprocess endpoints
- [ ] Simplified outbox
- [ ] README / architecture polish for demo and interviews
