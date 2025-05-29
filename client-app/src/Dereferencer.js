// utils/dereferenceJsonNet.js

export function dereferenceJsonNet(root) {
  if (typeof root !== "object" || root === null) return root;

  const refs = new Map();

  function recurse(obj) {
    if (obj === null || typeof obj !== "object") return obj;

    if (obj.$id) {
      if (refs.has(obj.$id)) return refs.get(obj.$id);
      const clone = Array.isArray(obj) ? [] : {};
      refs.set(obj.$id, clone);
      for (const key in obj) {
        if (key !== "$id") {
          clone[key] = recurse(obj[key]);
        }
      }
      return clone;
    }

    if (obj.$ref) {
      return refs.get(obj.$ref) || {};
    }

    if (Array.isArray(obj)) {
      return obj.map(recurse);
    }

    const newObj = {};
    for (const key in obj) {
      newObj[key] = recurse(obj[key]);
    }
    return newObj;
  }

  return recurse(root);
}
