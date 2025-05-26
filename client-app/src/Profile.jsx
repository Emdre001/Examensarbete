import React, { useEffect, useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { FaEye, FaEyeSlash, FaEnvelope } from 'react-icons/fa';
import './styles/Profile.css';

const MOCK_USER = true; // Set to false when backend is ready

const mockUserData = {
  email: "robinracho@outlook.com",
  password: "************",
  phone: "",
  birthdate: "1999-10-18",
  country: "Sweden",
  postalCode: "",
  city: "",
};

const countries = ["Sweden", "Norway", "Denmark", "Finland"];

const Profile = () => {
  const [user, setUser] = useState(MOCK_USER ? mockUserData : null);
  const [edit, setEdit] = useState({ password: false, phone: false });
  const [form, setForm] = useState(user || {});
  const [showDeleteConfirm, setShowDeleteConfirm] = useState(false);
  const [showPassword, setShowPassword] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    if (!MOCK_USER) {
      fetch('/api/profile', { credentials: 'include' })
        .then(res => {
          if (res.status === 401) {
            navigate('/login');
            return null;
          }
          return res.json();
        })
        .then(data => {
          if (data) {
            setUser(data);
            setForm(data);
          }
        });
    }
  }, [navigate]);

  useEffect(() => {
    setForm(user || {});
  }, [user]);

  if (!user) return <div className="profile-page">Loading...</div>;

  const handleChange = e => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSave = e => {
    e.preventDefault();
    setUser(form);
    setEdit({ password: false, phone: false });
    if (!MOCK_USER) {
      fetch('/api/profile', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        credentials: 'include',
        body: JSON.stringify(form),
      });
    }
  };

  const handleDelete = () => {
    setShowDeleteConfirm(false);
    if (!MOCK_USER) {
      fetch('/api/profile', {
        method: 'DELETE',
        credentials: 'include',
      }).then(() => navigate('/signup'));
    } else {
      setUser(null);
      navigate('/signup');
    }
  };

  return (
    <div className="profile-bg">
      <div className="profile-page">
        <h2 className="profile-title">Account Information</h2>
        <form className="profile-form" onSubmit={handleSave}>
          {/* Email with icon */}
          <label className="profile-label">
            Email*
            <div className="profile-input-icon-wrapper">
              <input
                className="profile-input"
                type="email"
                value={form.email}
                disabled
              />
              <FaEnvelope className="profile-input-icon" />
            </div>
          </label>

          {/* Password with eye icon */}
          <label className="profile-label profile-password-label">
            Password
            <div className="profile-input-icon-wrapper">
              <input
                className="profile-input"
                type={showPassword ? "text" : "password"}
                name="password"
                value={form.password}
                onChange={handleChange}
                disabled={!edit.password}
              />
              {showPassword ? (
                <FaEyeSlash
                  className="profile-input-icon profile-eye-icon"
                  onClick={() => setShowPassword(false)}
                  tabIndex={0}
                  aria-label="Hide password"
                  role="button"
                />
              ) : (
                <FaEye
                  className="profile-input-icon profile-eye-icon"
                  onClick={() => setShowPassword(true)}
                  tabIndex={0}
                  aria-label="Show password"
                  role="button"
                />
              )}
            </div>
            {!edit.password ? (
              <button
                type="button"
                className="profile-edit-link"
                onClick={() => setEdit({ ...edit, password: true })}
              >
                Edit
              </button>
            ) : (
              <button
                type="button"
                className="profile-edit-link"
                onClick={() => setEdit({ ...edit, password: false })}
              >
                Cancel
              </button>
            )}
          </label>

          {/* Phone number field, label on top */}
          <label className="profile-label">
            Phone Number
            <input
                className="profile-input"
                type="tel"
                name="phone"
                value={form.phone}
                onChange={handleChange}
                placeholder="Enter your phone number"
            />
            </label>

          <label className="profile-label">
            Birthdate*
            <input
              className="profile-input"
              type="date"
              name="birthdate"
              value={form.birthdate}
              onChange={handleChange}
              max={new Date().toISOString().split('T')[0]}
            />
          </label>

          <div className="profile-section-title">Location</div>
          <label className="profile-label">
            Country/Region*
            <select
              className="profile-input"
              name="country"
              value={form.country}
              onChange={handleChange}
            >
              {countries.map(c => <option key={c} value={c}>{c}</option>)}
            </select>
          </label>
          <label className="profile-label">
            Postal Code
            <input
              className="profile-input"
              type="text"
              name="postalCode"
              value={form.postalCode}
              onChange={handleChange}
            />
          </label>
          <label className="profile-label">
            City
            <input
              className="profile-input"
              type="text"
              name="city"
              value={form.city}
              onChange={handleChange}
            />
          </label>
          <button type="submit" className="profile-save-btn">Save Changes</button>
        </form>
        <hr className="profile-divider" />
        <div className="profile-delete-row">
          <span>Delete Account</span>
          <button
            className="profile-delete-btn"
            type="button"
            onClick={() => setShowDeleteConfirm(true)}
          >
            Remove
          </button>
        </div>
        {showDeleteConfirm && (
          <div className="profile-delete-confirm">
            <div>
              <strong>Are you sure you want to delete your account?</strong>
              <br />
              This action cannot be undone and your data will be permanently deleted.
            </div>
            <div className="profile-delete-confirm-actions">
              <button className="profile-delete-btn" onClick={handleDelete}>Yes, delete</button>
              <button className="profile-cancel-btn" onClick={() => setShowDeleteConfirm(false)}>Cancel</button>
            </div>
          </div>
        )}
        <div className="profile-actions">
          <Link to="/orders" className="profile-link">My Orders</Link>
          <Link to="/settings" className="profile-link">Settings</Link>
          <button className="profile-logout" onClick={() => {
            fetch('/api/logout', { method: 'POST', credentials: 'include' })
              .then(() => navigate('/login'));
          }}>Log out</button>
        </div>
      </div>
    </div>
  );
};

export default Profile;