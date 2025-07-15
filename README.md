# Warehouse Management System (WMS)

This project implements a **Warehouse Management System (WMS)** focused on **stock allocation, order cancellation, and SKU quantity correction**, designed using **Clean Architecture**, **DDD principles**, and **SOLID** rules.

---

## 🚀 **Features**

✅ Stock Allocation for customer orders  
✅ Priority-based and FIFO allocation  
✅ Complete delivery validation  
✅ Order cancellation (full or line item level)  
✅ SKU quantity correction with substitute SKU reallocation  
✅ Pagination for listing resources  
✅ Clean separation of layers (Domain, Application, Infrastructure, API)  
✅ AutoMapper integration for DTO mapping  
✅ Repository pattern abstraction  
✅ Dependency Injection centralized registration

---

## 🗂️ **Project Structure**
---

## 💡 **Key Concepts**

### 🔧 **Domain Layer**
- **Entities**: CustomerOrder, LineItem, SKU, Location
- **Aggregates**: CustomerOrder as root for LineItems, SKU as root for stock quantities
- **Enums/Value Objects**: Priority, status handling within business logic

### 🧩 **Application Layer**
- Implements **Use Cases** as services:
  - CustomerOrderService
  - SkuService
  - SkuQuantityCorrectionService
  - OrderCancellationService

- Defines **Interfaces** for repositories to decouple infrastructure.

### 🗄 **Infrastructure Layer**
- Implements **Repositories** using EF Core:
  - CustomerOrderRepository
  - SkuRepository

- Contains **DbContext (ApplicationDbContext)** for database operations.

### 🌐 **API Layer**
- Exposes RESTful endpoints for:
  - Customer orders (CRUD, allocation)
  - SKU (CRUD, quantity correction)
  - Order cancellation (order and line item level)

---

## 📝 **Use Case Example**

1. **Customer order is created.**
2. **Stock is allocated based on:**
   - Priority (High > Normal > Low)
   - FIFO for same priority
   - CompleteDeliveryRequired flag validation
3. **Order becomes ready to deliver.**
4. **If order or line item is cancelled, stock is released.**
5. **If SKU quantity is corrected and allocated stock is insufficient:**
   - Substitute SKU is searched.
   - If no substitute exists and CompleteDeliveryRequired is true, order is deallocated.

---

## ⚙️ **Run Locally**

```bash
# Restore packages
dotnet restore

# Apply migrations (if database used)
dotnet ef database update

# Run API
dotnet run --project AllocationStockManager.API
