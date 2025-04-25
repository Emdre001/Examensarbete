import { create } from 'zustand';

const useCartStore = create((set, get) => ({
  cart: [],
  addToCart: (product) => {
    const existingProduct = get().cart.find((item) => item.id === product.id);

    if (existingProduct) {
      set((state) => ({
        cart: state.cart.map((item) =>
          item.id === product.id
            ? { ...item, quantity: item.quantity + 1 }
            : item
        ),
      }));
    } else {
      set((state) => ({
        cart: [...state.cart, { ...product, quantity: 1 }],
      }));
    }
  },
  removeFromCart: (id) => {
    set((state) => ({
      cart: state.cart.filter((item) => item.id !== id),
    }));
  },
  updateQuantity: (id, quantity) => {
    if (quantity < 1) return; // Prevent quantity from going below 1
    set((state) => ({
      cart: state.cart.map((item) =>
        item.id === id ? { ...item, quantity } : item
      ),
    }));
  },
  clearCart: () => set({ cart: [] }),
}));

export default useCartStore;