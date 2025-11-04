// Jalankan dengan: node seed-data.js
// Pastikan MongoDB sudah running

const { MongoClient } = require('mongodb');

const url = 'mongodb://localhost:27017';
const dbName = 'SoraBrewDB';

const categories = [
  { name: "Hot Coffee", icon: "☕", order: 1 },
  { name: "Cup Coffee", icon: "🥤", order: 2 },
  { name: "Cold Coffee", icon: "🧊", order: 3 },
  { name: "Tea", icon: "🍵", order: 4 },
  { name: "Dessert", icon: "🍰", order: 5 }
];

const products = [
  {
    name: "Espresso Delight",
    description: "Indulge in the rich aroma and complex flavors of our meticulously crafted espresso drink.",
    price: 5.00,
    originalPrice: 10.00,
    discount: 50,
    category: "Hot Coffee",
    image: "https://images.unsplash.com/photo-1514432324607-a09d9b4aefdd?w=500",
    tags: ["Popular", "Hot", "Iced"],
    isPopular: true,
    createdAt: new Date()
  },
  {
    name: "Americano Bliss",
    description: "Enjoy the smooth and balanced taste of our chilled cold brew coffee, steeped for perfection.",
    price: 5.00,
    originalPrice: 10.00,
    discount: 50,
    category: "Cold Coffee",
    image: "https://images.unsplash.com/photo-1461023058943-07fcbe16d735?w=500",
    tags: ["Popular", "New", "Iced"],
    isPopular: true,
    createdAt: new Date()
  },
  {
    name: "Cappuccino Charm",
    description: "Experience the creamy froth and rich flavor of our expertly crafted cappuccino.",
    price: 3.50,
    category: "Cup Coffee",
    image: "https://images.unsplash.com/photo-1572442388796-11668a67e53d?w=500",
    tags: ["Seasonal", "Iced"],
    isPopular: false,
    createdAt: new Date()
  },
  {
    name: "Latte Luxury",
    description: "Experience the velvety blend of creamy steamed milk and our signature espresso.",
    price: 4.00,
    originalPrice: 8.00,
    discount: 50,
    category: "Hot Coffee",
    image: "https://images.unsplash.com/photo-1561882468-9110e03e0f78?w=500",
    tags: ["Popular", "Hot", "Iced"],
    isPopular: true,
    createdAt: new Date()
  },
  {
    name: "Mocha Magic",
    description: "Treat yourself to the rich, sweet and chocolatey goodness of our delicious mocha.",
    price: 4.00,
    category: "Cup Coffee",
    image: "https://images.unsplash.com/photo-1578374173705-0f48f5bd7b5c?w=500",
    tags: ["Seasonal", "Hot", "Sweet"],
    isPopular: false,
    createdAt: new Date()
  },
  {
    name: "Macchiato Marvel",
    description: "Savor the strong, sharp taste of espresso and milk in our expertly-crafted macchiato.",
    price: 4.50,
    category: "Cold Coffee",
    image: "https://images.unsplash.com/photo-1517487881594-2787fef5ebf7?w=500",
    tags: ["New", "Hot", "Sweet"],
    isPopular: false,
    createdAt: new Date()
  },
  {
    name: "Iced Coffee Bliss",
    description: "Cool down with our refreshing and bold cold brew coffee, perfectly smooth and satisfying.",
    price: 3.50,
    category: "Cold Coffee",
    image: "https://images.unsplash.com/photo-1517487881594-2787fef5ebf7?w=500",
    tags: ["Popular", "Hot", "Sweet"],
    isPopular: false,
    createdAt: new Date()
  },
  {
    name: "Cold Brew Dream",
    description: "Experience the rich and smooth flavors of our chilled cold brew coffee, brewed to perfection.",
    price: 4.00,
    category: "Cold Coffee",
    image: "https://images.unsplash.com/photo-1461023058943-07fcbe16d735?w=500",
    tags: ["New", "Hot", "Sweet"],
    isPopular: false,
    createdAt: new Date()
  },
  {
    name: "Cheesecake Delight",
    description: "Enjoy the creamy richness and indulgent flavor of our classic cheesecake.",
    price: 4.50,
    category: "Dessert",
    image: "https://images.unsplash.com/photo-1533134242974-e9c5afbb1217?w=500",
    tags: ["New", "Hot", "Sweet"],
    isPopular: false,
    createdAt: new Date()
  },
  {
    name: "Matcha Green Tea",
    description: "Savor the fresh, earthy, and smooth taste of our authentic matcha green tea.",
    price: 3.75,
    category: "Tea",
    image: "https://images.unsplash.com/photo-1564890369478-c89ca6d9cde9?w=500",
    tags: ["New", "Hot", "Sweet"],
    isPopular: false,
    createdAt: new Date()
  },
  {
    name: "Chai Tea Latte",
    description: "Delight in the spicy, creamy, and aromatic flavors of our chai tea latte, a perfect balance of spices.",
    price: 4.00,
    category: "Tea",
    image: "https://images.unsplash.com/photo-1597318130957-e6a1f96fed97?w=500",
    tags: ["Popular", "Hot", "Sweet"],
    isPopular: false,
    createdAt: new Date()
  },
  {
    name: "Chocolate Lava Cake",
    description: "Indulge in the warm, gooey center and decadent chocolate flavor of our lava cake.",
    price: 5.00,
    category: "Dessert",
    image: "https://images.unsplash.com/photo-1624353365286-3f8d62daad51?w=500",
    tags: ["Popular", "Hot", "Sweet"],
    isPopular: false,
    createdAt: new Date()
  }
];

// Admin user dengan password: admin123
const users = [
  {
    email: "admin@sorabrew.com",
    password: "$2a$10$8X3qiJZ.zWMOELvKvF5vPOYvGKW3Ld/QwFN2YXxJ.KqL9xH6VvDQO", // admin123
    fullName: "Admin User",
    phone: "+6281234567890",
    role: "admin",
    isActive: true,
    createdAt: new Date()
  },
  {
    email: "customer@example.com",
    password: "$2a$10$8X3qiJZ.zWMOELvKvF5vPOYvGKW3Ld/QwFN2YXxJ.KqL9xH6VvDQO", // admin123
    fullName: "John Doe",
    phone: "+6281234567891",
    role: "customer",
    isActive: true,
    createdAt: new Date()
  }
];

async function seedData() {
  const client = new MongoClient(url);

  try {
    await client.connect();
    console.log('Connected to MongoDB');

    const db = client.db(dbName);

    // Clear existing data
    await db.collection('categories').deleteMany({});
    await db.collection('products').deleteMany({});
    await db.collection('users').deleteMany({});

    // Insert categories
    await db.collection('categories').insertMany(categories);
    console.log('Categories inserted');

    // Insert products
    await db.collection('products').insertMany(products);
    console.log('Products inserted');

    // Insert users
    await db.collection('users').insertMany(users);
    console.log('Users inserted');

    console.log('Database seeded successfully!');
    console.log('\nLogin Credentials:');
    console.log('Admin: admin@sorabrew.com / admin123');
    console.log('Customer: customer@example.com / admin123');

  } catch (error) {
    console.error('Error seeding database:', error);
  } finally {
    await client.close();
  }
}

seedData();