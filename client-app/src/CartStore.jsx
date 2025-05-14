import { create } from 'zustand';

const useCartStore = create((set, get) => ({
  cart: [],
  addToCart: (product) => {
    const existingProduct = get().cart.find(
      (item) => item.id === product.id && item.size === product.size
    );

    if (existingProduct) {
      set((state) => ({
        cart: state.cart.map((item) =>
          item.id === product.id && item.size === product.size
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
  removeFromCart: (id, size) => {
    set((state) => ({
      cart: state.cart.filter((item) => item.id !== id || item.size !== size),
    }));
  },
  updateQuantity: (id, size, quantity) => {
    if (quantity < 1) return;
    set((state) => ({
      cart: state.cart.map((item) =>
        item.id === id && item.size === size ? { ...item, quantity } : item
      ),
    }));
  },
  clearCart: () => set({ cart: [] }),
}));

export default useCartStore;