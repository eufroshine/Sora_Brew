# ☕ Sora Brew - Coffee Shop E-Commerce Platform

![Sora Brew Banner](https://images.unsplash.com/photo-1495474472287-4d71bcdd2085?w=1200&h=300&fit=crop)

A modern, full-stack e-commerce web application for a coffee shop built with **ASP.NET Core**, **Vue.js**, and **MongoDB**.

## 📖 About Sora Brew

**Sora Brew** (空 Brew) combines the Japanese word "Sora" (sky/heaven) symbolizing premium quality, with "Brew" representing our core coffee-making expertise. This platform provides a seamless online ordering experience for coffee lovers, complete with product management, shopping cart, checkout process, and comprehensive admin dashboard.

### 🎯 Project Purpose

Developed as a comprehensive demonstration of modern full-stack web development practices, Sora Brew showcases:
- RESTful API architecture with JWT authentication
- Real-time cart management and order processing
- Role-based access control (Customer & Admin)
- Responsive, user-friendly interface design
- Production-ready code structure and best practices

---

## 🚀 Tech Stack

### Frontend
- **Vue.js 3** - Progressive JavaScript framework
- **Vuex 4** - State management
- **Vue Router 4** - Client-side routing
- **Axios** - HTTP client for API communication
- **CSS3/SCSS** - Styling with modern design principles

### Backend
- **ASP.NET Core 8** - Web API framework
- **MongoDB.Driver** - NoSQL database integration
- **JWT Bearer Authentication** - Secure token-based auth
- **BCrypt.Net** - Password hashing
- **Swagger/OpenAPI** - API documentation

### Database
- **MongoDB 7.0** - NoSQL document database
- **MongoDB Compass** - Database management GUI

### Development Tools
- **Git** - Version control
- **Postman** - API testing
- **Visual Studio Code** - Code editor
- **npm/dotnet CLI** - Package management

---

## ✨ Features

### 🛍️ Customer Features
- **User Authentication** - Register and login with JWT tokens
- **Browse Products** - View coffee menu with categories and search
- **Shopping Cart** - Add, update, and remove items with real-time totals
- **Checkout Process** - Complete order with delivery information
- **Order History** - Track all past and current orders
- **Responsive Design** - Optimized for desktop, tablet, and mobile

### 👨‍💼 Admin Features
- **Dashboard** - Overview statistics (products, orders, revenue)
- **Product Management** - Full CRUD operations for products
- **Order Management** - View and update order statuses
- **Category Management** - Organize products by categories
- **User Management** - View registered users and roles

### 🔒 Security Features
- JWT-based authentication & authorization
- Password hashing with BCrypt
- Role-based access control (RBAC)
- CORS configuration for frontend integration
- Secure API endpoints with middleware validation

---

## 📋 Business Process

### Customer Journey
```
Registration/Login → Browse Products → Add to Cart → Checkout 
→ Place Order → Payment Selection → Order Confirmation → Track Order
```

### Admin Workflow
```
Admin Login → View Dashboard → Manage Products (CRUD) 
→ Process Orders → Update Order Status → Monitor Sales
```

---

## 🗄️ Data Model

### Collections Structure

**Users**
- Email, Password (hashed), Full Name, Phone, Address
- Role: `customer` | `admin`
- Authentication & Profile Management

**Products**
- Name, Description, Price, Discount, Category
- Image URL, Tags, Popular Flag
- Product Catalog Management

**Categories**
- Name, Icon (emoji), Display Order
- Hot Coffee ☕, Cold Coffee 🧊, Tea 🍵, Dessert 🍰

**Orders**
- Order Number, User Reference, Order Items
- Subtotal, Tax (10%), Total Amount
- Status: `pending` | `processing` | `completed` | `cancelled`
- Delivery Information & Payment Method

---

## 🎨 UI/UX Design

### Design System
- **Primary Color**: #8b5e3c (Coffee Brown)
- **Typography**: System fonts (Roboto, SF Pro, Segoe UI)
- **Layout**: Modern, clean, card-based design
- **Components**: Reusable Vue components
- **Animations**: Smooth transitions and hover effects

### Key Pages
1. **Homepage** - Hero section, featured products, testimonials
2. **Menu** - Product catalog with filters and search
3. **Cart** - Shopping cart with quantity controls
4. **Checkout** - Order form and summary
5. **Admin Dashboard** - Statistics and management tools

---

## 🛠️ Installation & Setup

### Prerequisites
- Node.js 18+ and npm
- .NET 8 SDK
- MongoDB 7.0+
- Git

### Backend Setup

```bash
# Clone repository
git clone https://github.com/yourusername/sora-brew.git
cd sora-brew/backend/SoraBrewAPI

# Install packages
dotnet restore

# Configure appsettings.json
# Update MongoDB connection string and JWT secret

# Run migrations/seed data
# (Optional: Run seed-data.js to populate initial data)

# Start backend server
dotnet run
# API runs at: https://localhost:7000
```

### Frontend Setup

```bash
# Navigate to frontend
cd ../../frontend

# Install dependencies
npm install

# Configure API endpoint
# Update src/services/api.js with backend URL

# Start development server
npm run serve
# App runs at: http://localhost:8080
```

### Database Setup

```bash
# Start MongoDB service
mongod

# Create database and collections
# Use MongoDB Compass or shell
use SoraBrewDB

# Seed initial data (optional)
node seed-data.js
```

---

## 🔑 Configuration

### Backend (appsettings.json)
```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "SoraBrewDB"
  },
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKey",
    "Issuer": "SoraBrewAPI",
    "Audience": "SoraBrewClient"
  }
}
```

### Frontend (.env)
```env
VUE_APP_API_URL=https://localhost:7000/api
```

---

## 📡 API Endpoints

### Authentication
```
POST   /api/auth/register    - Register new user
POST   /api/auth/login       - Login user
GET    /api/auth/me          - Get current user
```

### Products
```
GET    /api/products         - Get all products
GET    /api/products/{id}    - Get product by ID
POST   /api/products         - Create product (Admin)
PUT    /api/products/{id}    - Update product (Admin)
DELETE /api/products/{id}    - Delete product (Admin)
```

### Orders
```
GET    /api/orders           - Get orders (filtered by user/admin)
GET    /api/orders/{id}      - Get order details
POST   /api/orders           - Create new order
PUT    /api/orders/{id}/status - Update order status (Admin)
```

### Categories
```
GET    /api/categories       - Get all categories
POST   /api/categories       - Create category (Admin)
```

📚 **Full API Documentation**: Available at `https://localhost:7000/swagger`

---

## 👥 Default Credentials

### Admin Account
```
Email: admin@sorabrew.com
Password: admin123
```

### Test Customer Account
```
Email: customer@example.com
Password: admin123
```

---

## 📁 Project Structure

```
sora-brew/
├── backend/
│   └── SoraBrewAPI/
│       ├── Controllers/
│       │   ├── AuthController.cs
│       │   ├── ProductsController.cs
│       │   ├── CategoriesController.cs
│       │   └── OrdersController.cs
│       ├── Models/
│       │   ├── User.cs
│       │   ├── Product.cs
│       │   ├── Category.cs
│       │   └── Order.cs
│       ├── Services/
│       │   ├── MongoDbService.cs
│       │   └── JwtService.cs
│       ├── Program.cs
│       └── appsettings.json
│
├── frontend/
│   ├── public/
│   └── src/
│       ├── components/
│       │   ├── Hero.vue
│       │   ├── CategoryNav.vue
│       │   └── ProductCard.vue
│       ├── views/
│       │   ├── Home.vue
│       │   ├── Menu.vue
│       │   ├── Cart.vue
│       │   ├── Checkout.vue
│       │   ├── Orders.vue
│       │   ├── Login.vue
│       │   └── Admin/
│       │       ├── AdminLayout.vue
│       │       ├── Dashboard.vue
│       │       ├── Products.vue
│       │       └── Orders.vue
│       ├── store/
│       │   ├── index.js
│       │   └── modules/
│       │       ├── auth.js
│       │       └── cart.js
│       ├── services/
│       │   └── api.js
│       ├── router/
│       │   └── index.js
│       └── App.vue
│
└── seed-data.js
```

---

## 🧪 Testing

### Backend Testing
```bash
# Run unit tests
dotnet test

# Test API with Swagger
# Navigate to https://localhost:7000/swagger
```

### Frontend Testing
```bash
# Run unit tests
npm run test:unit

# Run E2E tests
npm run test:e2e
```

---

## 🚀 Deployment

### Backend (Azure/AWS/Heroku)
1. Configure production connection strings
2. Enable HTTPS and SSL certificates
3. Set environment variables
4. Deploy API with `dotnet publish`

### Frontend (Vercel/Netlify)
1. Build production bundle: `npm run build`
2. Configure environment variables
3. Deploy `dist/` folder

### Database (MongoDB Atlas)
1. Create cluster on MongoDB Atlas
2. Update connection string in backend
3. Configure IP whitelist and security

---

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Code Style
- Backend: Follow C# coding conventions
- Frontend: Use ESLint and Prettier
- Commit messages: Use conventional commits format

---

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 👨‍💻 Development Team

**Team Roles:**
- Project Manager - Overall coordination and planning
- Backend Developer - ASP.NET Core API development
- Frontend Developer - Vue.js UI/UX implementation
- Database Designer - MongoDB schema and optimization
- QA Engineer - Testing and quality assurance

---

## 🔮 Future Enhancements

- [ ] Payment gateway integration (Stripe/PayPal)
- [ ] Email notifications for order updates
- [ ] Product reviews and ratings system
- [ ] Wishlist functionality
- [ ] Real-time order tracking with WebSockets
- [ ] Mobile app (React Native/Flutter)
- [ ] Multi-language support (i18n)
- [ ] Advanced analytics dashboard
- [ ] Loyalty points and rewards program
- [ ] Social media integration

---

## 📞 Contact & Support

- **Email**: support@sorabrew.com
- **Website**: https://sorabrew.com
- **Issues**: [GitHub Issues](https://github.com/yourusername/sora-brew/issues)
- **Documentation**: [Wiki](https://github.com/yourusername/sora-brew/wiki)

---

## 🙏 Acknowledgments

- Coffee images from [Unsplash](https://unsplash.com)
- Icons from [Heroicons](https://heroicons.com)
- Design inspiration from modern e-commerce platforms
- Community support from Stack Overflow and GitHub

---

## 📊 Project Stats

![GitHub repo size](https://img.shields.io/github/repo-size/yourusername/sora-brew)
![GitHub stars](https://img.shields.io/github/stars/yourusername/sora-brew)
![GitHub forks](https://img.shields.io/github/forks/yourusername/sora-brew)
![GitHub issues](https://img.shields.io/github/issues/yourusername/sora-brew)
![GitHub license](https://img.shields.io/github/license/yourusername/sora-brew)

---

<div align="center">
  <p>Made with ☕ and ❤️ by Sora Brew Team</p>
  <p>⭐ Star this repo if you find it helpful!</p>
</div>
