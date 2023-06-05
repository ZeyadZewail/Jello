import "./globals.css";

export const metadata = {
	title: "Kanban Z",
	description: "The ultimate Kanban board",
};

export default function RootLayout({ children }: { children: React.ReactNode }) {
	return (
		<html lang="en">
			<body className="flex min-h-screen flex-col items-center bg-secondary text-primary">{children}</body>
		</html>
	);
}
