import { useEffect, useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { FaEye, FaEyeSlash, FaEnvelope } from 'react-icons/fa';
import './styles/Profile.css';

const Profile = () => {
  const [user, setUser] = useState(null);
  const [edit, setEdit] = useState({ password: false, phone: false });
  const [form, setForm] = useState({});
  const [showDeleteConfirm, setShowDeleteConfirm] = useState(false);
  const [showPassword, setShowPassword] = useState(false);
  const navigate = useNavigate();



  useEffect(() => {
    const credentials = localStorage.getItem('auth');
    fetch('http://localhost:5066/api/Account/GetCurrentUser', { 
      headers: {
        'Authorization': `Basic ${credentials}`
      },
    })
    .then(async res => {
      if (res.status === 401) {
        navigate('/login');
        return null;
      }
      if (!res.ok) {
        const text = await res.text();
        throw new Error(text);
      }
      return res.json();
    })
    .then(data => {
      if (data) {
        setUser(data);
        setForm(data);
      }
    })
    .catch(err => {
      console.error('Failed to load profile:', err.message);
    });;
  }, [navigate]);

  useEffect(() => {
    setForm(user || {});
  }, [user]);

  if (!user) return <div className="profile-page">Loading...</div>;

  const handleChange = e => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSave = async e => {
    e.preventDefault();
    setEdit({ password: false, phone: false });

    const payload = {
    UserName: form.userName || form.UserName || "",
    Email: form.email || form.userEmail || "",
    Address: form.address || form.userAddress || "",
    PhoneNumber: form.phone || form.userPhoneNr || ""
    };

    const credentials = localStorage.getItem('auth');

    try {
      const response = await fetch(`http://localhost:5066/api/Account/UpdateAccount/${user.userId || user.UserId}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Basic ${credentials}`
        },
        body: JSON.stringify(payload),
      });
      if (!response.ok) {
        const errorMsg = await response.text();
        alert(`Failed to update profile: ${errorMsg}`);
        return;
      }

      setUser({ ...user, ...payload });
      alert('Profile updated successfully');
    } catch (error) {
      alert("Error updating profile.");
    }
  };

  const handleDelete = async () => {
    setShowDeleteConfirm(false);
    const credentials = localStorage.getItem('auth');
    try {
      const response = await fetch(
        `http://localhost:5066/api/Account/DeleteAccount/${user.userId || user.UserId}`,
        {
          method: 'DELETE',
          headers: {
            'Authorization': `Basic ${credentials}`,
          },
        }
      );
      if (!response.ok) {
        const errorMsg = await response.text();
        alert(`Failed to delete account: ${errorMsg}`);
        return;
      }

      setUser(null);
      alert('Account deleted successfully');
      navigate('/signup');
    } catch (error) {
      alert(`Error deleting account: ${error}`);
    }
  };

  return (
    <div className="profile-bg">
      <div className="profile-page">
        <h2 className="profile-title">Account Information</h2>
        <form className="profile-form" onSubmit={handleSave}>
          {/* Username field */}
          <label className="profile-label">
            Username*
            <input
              className="profile-input"
              type="text"
              name="userName"
              value={form.userName || form.UserName || ""}
              onChange={handleChange}
              required
            />
          </label>

          {/* Email with icon */}
          <label className="profile-label">
            Email*
            <div className="profile-input-icon-wrapper">
              <input
                className="profile-input"
                type="email"
                value={form.email || form.userEmail || ""}
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
            Address
            <input
              className="profile-input"
              type="text"
              name="address"
              value={form.address ?? ""}
              onChange={handleChange}
              placeholder="Enter your address"
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
            onClick={() => setShowDeleteConfirm(true)}>
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
        </div>
      </div>
    </div>
  );
};

export default Profile;