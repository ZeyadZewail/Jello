"use client";

import Spinner from "../../components/spinner/Spinner";
import { useRef, useState } from "react";

const LoginForm = () => {
	const formRef = useRef(null);
	const [form, setForm] = useState({ email: "", password: "" });
	const [loading, setLoading] = useState(false);

	const handleChange = (e: React.FormEvent<HTMLInputElement>) => {
		const target = e.target as HTMLInputElement;
		const { name, value } = target;

		setForm({ ...form, [name]: value });
	};

	const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
		e.preventDefault();
		setLoading(true);
	};

	return (
		<div className="flex flex-col shadow-md p-6 gap-2 bg-opacity bg-primary bg-opacity-5 rounded-md">
			<h2 className="text-4xl text-accent text-center">Kanban Z</h2>
			<p className="text-center"> Log in to continue </p>
			<form ref={formRef} onSubmit={handleSubmit} className="flex flex-col gap-4">
				<label className="flex flex-col">
					<span className="font-medium mb-2">Email</span>
					<input
						type="email"
						name="email"
						value={form.email}
						onChange={handleChange}
						placeholder="Email"
						className="py-2 px-6 text-secondary rounded-lg outlined-none border-none font-medium"
						autoComplete="hidden"
					/>
				</label>
				<label className="flex flex-col">
					<span className="font-medium mb-2">Password</span>
					<input
						type="password"
						name="password"
						value={form.password}
						onChange={handleChange}
						placeholder="Password"
						className=" py-2 px-6 text-secondary rounded-lg outlined-none border-none font-medium"
						autoComplete="hidden"
					/>
				</label>
				<button
					type="submit"
					className="py-3 w-24 justify-center flex px-8 min-w-30 bg-primary-button font-bold rounded-xl self-center hover:bg-opacity-80">
					{loading ? <Spinner size={24} /> : "Login"}
				</button>
			</form>
		</div>
	);
};

export default LoginForm;
